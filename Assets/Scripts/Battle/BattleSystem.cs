using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public Transform handPanel;
    public GameObject cardPrefab;
    public Button endTurnButton;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyHPText;
    public PlayerStats playerStats;
    public TextMeshProUGUI energyText;
    public GameObject winPanel;
    public Button ReturnButton;
    public GameObject losePanel;
    public GameObject victoryPanel;  // Boss战胜利后显示的通关界面

    private List<CardData> deck = new List<CardData>();
    private List<CardData> handCards = new List<CardData>();
    private List<CardData> discardPile = new List<CardData>();

    private int currentEnergy;
    private const int MAX_ENERGY = 3;
    private int bonusDefense = 0;

    private const int MAX_HAND_SIZE = 5;

    private EnemyData currentEnemy;
    private bool isPlayerTurn = true;
    private int persistentBonusDamage = 0; // 商店购买加成
    private int tempBonusDamage = 0;       // 技能临时加成

    private bool isInvisible = false;
    private int bonusDamage = 0;
    private bool doubleNextAttack = false;

    void Start()
    {
        CharacterData character = PlayerData.selectedCharacter;
        if (character == null)
        {
            Debug.LogError("无法获取角色数据！");
            return;
        }
        Debug.Log("当前角色名：" + character.className);
        currentEnergy = MAX_ENERGY;
        UpdateEnergyUI();

        deck = CardDatabase.GetInitialDeck(character.className);
        Shuffle(deck);
        DrawCards(MAX_HAND_SIZE);

        persistentBonusDamage = GameData.Instance.tempBonusDamageNextBattle;
        GameData.Instance.tempBonusDamageNextBattle = 0;


        // 设置敌人，根据场景选择是否是 Boss
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "BossScene")
        {
            currentEnemy = EnemyDatabase.GetBossEnemy();
        }
        else
        {
            currentEnemy = EnemyDatabase.GetRandomEnemy();
        }

        Debug.Log("敌人已加载：" + currentEnemy.enemyName);
        UpdateEnemyUI();

        // 绑定回合结束按钮
        if (endTurnButton != null)
            endTurnButton.onClick.AddListener(EndTurn);
        if (ReturnButton != null)
            ReturnButton.onClick.AddListener(ReturnToMainMenu);

    }


    void Shuffle(List<CardData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            CardData temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0)
            {
                if (discardPile.Count == 0) break;
                deck.AddRange(discardPile);
                discardPile.Clear();
                Shuffle(deck);
            }

            CardData card = deck[0];
            deck.RemoveAt(0);
            handCards.Add(card);
            Debug.Log($"抽取卡牌：{card.cardName} ({card.type}) - 描述: {card.description}");
        }

        UpdateHandDisplay();
    }

    void UpdateHandDisplay()
    {
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (CardData card in handCards)
        {
            GameObject cardObj = Instantiate(cardPrefab, handPanel);
            cardObj.transform.Find("CardName").GetComponent<Text>().text = card.cardName;
            cardObj.transform.Find("Description").GetComponent<Text>().text = card.description;
            cardObj.transform.Find("Type").GetComponent<Text>().text = card.type;
            cardObj.transform.Find("EnergyCostText").GetComponent<TextMeshProUGUI>().text = card.energyCost.ToString();

            Button btn = cardObj.GetComponent<Button>();
            btn.onClick.AddListener(() => OnCardPlayed(card));
        }
    }

    void OnCardPlayed(CardData card)
    {
        if (card.energyCost > currentEnergy)
        {
            Debug.Log("能量不足，无法使用此卡牌！");
            return;
        }

        currentEnergy -= card.energyCost;
        UpdateEnergyUI();

        Debug.Log("使用卡牌：" + card.cardName);

        if (card.type == "Attack" && currentEnemy != null)
        {
            int totalDamage = card.value + persistentBonusDamage + tempBonusDamage + bonusDamage;

            if (doubleNextAttack)
            {
                totalDamage *= 2;
                doubleNextAttack = false;
            }
            currentEnemy.currentHP -= totalDamage;
            if (currentEnemy.currentHP < 0) currentEnemy.currentHP = 0;

            Debug.Log($"造成 {totalDamage} 点伤害（基础 {card.value} + 临时加成 {tempBonusDamage} + 商店加成 {persistentBonusDamage}）");

            CheckBattleEnd();
        }
        else if (card.type == "Defense" && playerStats != null)
        {
            int totalBlock = card.value + bonusDefense;
            playerStats.GainBlock(totalBlock);
            Debug.Log($"防御卡产生格挡：{totalBlock}（基础 {card.value} + 加成 {bonusDefense}）");
        }

        else if (card.type == "Skill")
        {
            foreach (SkillEffectEntry effect in card.skillEffects)
            {
                switch (effect.effectType)
                {
                    case SkillEffectType.DrawCards:
                        DrawCards(effect.effectValue);
                        Debug.Log($"技能效果：抽 {effect.effectValue} 张卡");
                        break;
                    case SkillEffectType.GainEnergy:
                        currentEnergy += effect.effectValue;
                        Debug.Log($"技能效果：获得 {effect.effectValue} 点能量");
                        break;
                    case SkillEffectType.Invisibility:
                        isInvisible = true;
                        Debug.Log("技能效果：进入隐身状态（本回合免疫攻击）");
                        break;
                    case SkillEffectType.TempStrength:
                        tempBonusDamage += effect.effectValue;
                        Debug.Log($"技能效果：攻击力 +{effect.effectValue}（本回合）");
                        break;
                    case SkillEffectType.DoubleNextAttack:
                        doubleNextAttack = true;
                        Debug.Log("技能效果：下次攻击造成双倍伤害");
                        break;
                    case SkillEffectType.Heal:
                        Debug.Log($"技能效果：治疗 {effect.effectValue} 点（未实现）");
                        break;
                    case SkillEffectType.TempDefenseBoost:
                        bonusDefense += effect.effectValue;
                        Debug.Log($"技能效果：防御力 +{effect.effectValue}（本回合）");
                        break;
                    case SkillEffectType.None:
                    default:
                        break;
                }
            }
        }

        handCards.Remove(card);
        discardPile.Add(card);
        UpdateHandDisplay();
        UpdateEnemyUI();
    }

    void EndTurn()
    {
        if (!isPlayerTurn) return;

        isPlayerTurn = false;

        tempBonusDamage = 0;
        bonusDamage = 0;
        bonusDefense = 0;
        doubleNextAttack = false;


        Debug.Log("玩家结束回合 → 敌人行动");

        discardPile.AddRange(handCards);
        handCards.Clear();
        UpdateHandDisplay();

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        if (isInvisible)
        {
            Debug.Log("敌人攻击被闪避（隐身）");
            isInvisible = false;
        }
        else if (playerStats != null)
        {
            Debug.Log($"{currentEnemy.enemyName} 攻击玩家，造成 {currentEnemy.attackDamage} 点伤害");
            playerStats.TakeDamage(currentEnemy.attackDamage);
            CheckBattleEnd();
        }

        yield return new WaitForSeconds(1f);

        isPlayerTurn = true;
        DrawCards(MAX_HAND_SIZE);
        currentEnergy = MAX_ENERGY;
        UpdateEnergyUI();
    }

    void UpdateEnergyUI()
    {
        if (energyText != null)
            energyText.text = $"Energy: {currentEnergy}/{MAX_ENERGY}";
    }

    void UpdateEnemyUI()
    {
        if (enemyNameText != null)
            enemyNameText.text = currentEnemy.enemyName;

        if (enemyHPText != null)
            enemyHPText.text = $"HP: {currentEnemy.currentHP}/{currentEnemy.maxHP}";
    }

    void CheckBattleEnd()
    {
        if (currentEnemy != null && currentEnemy.currentHP <= 0)
        {
            EndBattle(true); // 胜利
        }
        else if (playerStats.currentHP <= 0)
        {
            EndBattle(false); // 失败
        }
    }

    public void EndBattle(bool isWin)
    {
        if (isWin)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "BossScene" && victoryPanel != null)
            {
                victoryPanel.SetActive(false); // 打败 Boss，显示通关界面
            }
            else
            {
                winPanel.SetActive(true); // 普通胜利
            }
        }
        else
        {
            SceneManager.LoadScene("DeathTransition");
        }
    }

    public void OnWinContinueButton()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        PlayerData.selectedCharacter.currentHP = playerStats.currentHP;
        SceneManager.LoadScene("Map");
    }


    public void ReturnToMainMenu()
    {
        ResetGameData(); // 新加的
        SceneManager.LoadScene("MainMenu"); // 你的主菜单场景名
    }
    void ResetGameData()
    {
        // 重置角色数据
        if (PlayerData.selectedCharacter != null)
        {
            PlayerData.selectedCharacter.currentHP = PlayerData.selectedCharacter.maxHP;
            //character.currentHP = character.maxHP;
        }

        // 重置 GameData
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetData();
        }


        Debug.Log("游戏数据已重置");
    }


}
