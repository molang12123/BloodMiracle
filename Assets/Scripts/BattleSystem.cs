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
    public GameObject victoryPanel;  // Bossսʤ������ʾ��ͨ�ؽ���

    private List<CardData> deck = new List<CardData>();
    private List<CardData> handCards = new List<CardData>();
    private List<CardData> discardPile = new List<CardData>();

    private int currentEnergy;
    private const int MAX_ENERGY = 3;
    private int bonusDefense = 0;

    private const int MAX_HAND_SIZE = 5;

    private EnemyData currentEnemy;
    private bool isPlayerTurn = true;

    private bool isInvisible = false;
    private int bonusDamage = 0;
    private bool doubleNextAttack = false;

    void Start()
    {
        CharacterData character = SelectedCharacter.Instance?.characterData;
        if (character == null)
        {
            Debug.LogError("�޷���ȡ��ɫ���ݣ�");
            return;
        }

        currentEnergy = MAX_ENERGY;
        UpdateEnergyUI();

        deck = CardDatabase.GetInitialDeck(character.className);
        Shuffle(deck);
        DrawCards(MAX_HAND_SIZE);

        bonusDamage += GameData.Instance.tempBonusDamageNextBattle;
        GameData.Instance.tempBonusDamageNextBattle = 0; // ֻ����һ����Ч

        // ���õ��ˣ����ݳ���ѡ���Ƿ��� Boss
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "BossScene")
        {
            currentEnemy = EnemyDatabase.GetBossEnemy();
        }
        else
        {
            currentEnemy = EnemyDatabase.GetRandomEnemy();
        }

        Debug.Log("�����Ѽ��أ�" + currentEnemy.enemyName);
        UpdateEnemyUI();

        // �󶨻غϽ�����ť
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
            Debug.Log("�������㣬�޷�ʹ�ô˿��ƣ�");
            return;
        }

        currentEnergy -= card.energyCost;
        UpdateEnergyUI();

        Debug.Log("ʹ�ÿ��ƣ�" + card.cardName);

        if (card.type == "Attack" && currentEnemy != null)
        {
            int totalDamage = card.value + bonusDamage;
            if (doubleNextAttack)
            {
                totalDamage *= 2;
                doubleNextAttack = false;
            }
            currentEnemy.currentHP -= totalDamage;
            if (currentEnemy.currentHP < 0) currentEnemy.currentHP = 0;

            Debug.Log($"��� {totalDamage} ���˺������� {card.value} + �ӳ� {bonusDamage}��");

            CheckBattleEnd();
        }
        else if (card.type == "Defense" && playerStats != null)
        {
            int totalBlock = card.value + bonusDefense;
            playerStats.GainBlock(totalBlock);
            Debug.Log($"�����������񵲣�{totalBlock}������ {card.value} + �ӳ� {bonusDefense}��");
        }

        else if (card.type == "Skill")
        {
            foreach (SkillEffectEntry effect in card.skillEffects)
            {
                switch (effect.effectType)
                {
                    case SkillEffectType.DrawCards:
                        DrawCards(effect.effectValue);
                        Debug.Log($"����Ч������ {effect.effectValue} �ſ�");
                        break;
                    case SkillEffectType.GainEnergy:
                        currentEnergy += effect.effectValue;
                        Debug.Log($"����Ч������� {effect.effectValue} ������");
                        break;
                    case SkillEffectType.Invisibility:
                        isInvisible = true;
                        Debug.Log("����Ч������������״̬�����غ����߹�����");
                        break;
                    case SkillEffectType.TempStrength:
                        bonusDamage += effect.effectValue;
                        Debug.Log($"����Ч���������� +{effect.effectValue}�����غϣ�");
                        break;
                    case SkillEffectType.DoubleNextAttack:
                        doubleNextAttack = true;
                        Debug.Log("����Ч�����´ι������˫���˺�");
                        break;
                    case SkillEffectType.Heal:
                        Debug.Log($"����Ч�������� {effect.effectValue} �㣨δʵ�֣�");
                        break;
                    case SkillEffectType.TempDefenseBoost:
                        bonusDefense += effect.effectValue;
                        Debug.Log($"����Ч���������� +{effect.effectValue}�����غϣ�");
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

        bonusDamage = 0;

        Debug.Log("��ҽ����غ� �� �����ж�");

        discardPile.AddRange(handCards);
        handCards.Clear();
        UpdateHandDisplay();
        bonusDamage = 0;
        bonusDefense = 0;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        if (isInvisible)
        {
            Debug.Log("���˹��������ܣ�����");
            isInvisible = false;
        }
        else if (playerStats != null)
        {
            Debug.Log($"{currentEnemy.enemyName} ������ң���� {currentEnemy.attackDamage} ���˺�");
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
            EndBattle(true); // ʤ��
        }
        else if (playerStats.currentHP <= 0)
        {
            EndBattle(false); // ʧ��
        }
    }

    public void EndBattle(bool isWin)
    {
        if (isWin)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "BossScene" && victoryPanel != null)
            {
                victoryPanel.SetActive(true); // ��� Boss����ʾͨ�ؽ���
            }
            else
            {
                winPanel.SetActive(true); // ��ͨʤ��
            }
        }
        else
        {
            losePanel.SetActive(true); // ʧ�ܽ���
        }
    }

    public void OnWinContinueButton()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        SelectedCharacter.Instance.characterData.currentHP = playerStats.currentHP;

        SceneManager.LoadScene("Map");
    }


    public void ReturnToMainMenu()
    {
        ResetGameData(); // �¼ӵ�
        SceneManager.LoadScene("MainMenu"); // ������˵�������
    }
    void ResetGameData()
    {
        // ���ý�ɫ����
        if (SelectedCharacter.Instance != null && SelectedCharacter.Instance.characterData != null)
        {
            var character = SelectedCharacter.Instance.characterData;
            character.currentHP = character.maxHP;
        }

        // ���� GameData
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetData();
        }


        Debug.Log("��Ϸ����������");
    }


}
