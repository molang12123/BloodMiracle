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
        if (GameData.Instance.SpendGold(50))
        {
            GameData.Instance.tempBonusDamageNextBattle += 3;
            Debug.Log("����ɹ�����һ��ս������ +3");
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("��Ҳ��㣡");
        }
    }


    void BuyHeal()
    {
        if (GameData.Instance.SpendGold(30))
        {
            GameData.Instance.currentHP = Mathf.Min(
                GameData.Instance.currentHP + 15,
                GameData.Instance.maxHP
            );

            var stats = FindObjectOfType<PlayerStats>();
            if (stats != null)
            {
                stats.currentHP = GameData.Instance.currentHP;
                stats.UpdateUI();
            }

            Debug.Log("����ɹ����ָ� 15 ������ֵ");
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("��Ҳ��㣡");
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