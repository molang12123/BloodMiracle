using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSceneManager : MonoBehaviour
{
    public Button returnButton;
    public Button damageBoostButton;
    public Button healButton;
    public TextMeshProUGUI goldText;

    void Start()
    {
        returnButton.onClick.AddListener(HandleReturnToMap);
        damageBoostButton.onClick.AddListener(BuyDamageBoost);
        healButton.onClick.AddListener(BuyHeal);
        UpdateGoldUI();
    }

    void HandleReturnToMap()
    {
        SceneManager.LoadScene("Map");
    }

    void BuyDamageBoost()
    {
        if (GameData.Instance.gold >= 50)
        {
            GameData.Instance.gold -= 50;
            GameData.Instance.tempBonusDamageNextBattle += 3;
            Debug.Log("购买成功：下一场战斗攻击 +3");
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("金币不足！");
        }
    }

    void BuyHeal()
    {
        if (GameData.Instance.gold >= 30)
        {
            GameData.Instance.gold -= 30;

            // 增加 currentHP，不超过 maxHP
            GameData.Instance.currentHP = Mathf.Min(
                GameData.Instance.currentHP + 15,
                GameData.Instance.maxHP
            );

            //同步到 PlayerStats（如果当前场景中存在该组件）
            var stats = FindObjectOfType<PlayerStats>();
            if (stats != null)
            {
                stats.currentHP = GameData.Instance.currentHP;
                stats.UpdateUI();
            }

            Debug.Log("购买成功：恢复 15 点生命值");
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("金币不足！");
        }
    }
    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = $"Gold: {GameData.Instance.gold}";
        }
    }
}




