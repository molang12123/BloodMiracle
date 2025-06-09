using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestPointSceneManager : MonoBehaviour
{
    public Button healButton;
    public TextMeshProUGUI healText;

    private void Start()
    {
        healButton.onClick.AddListener(HealAndReturnToMap);
        UpdateHealText();
    }

    void HealAndReturnToMap()
    {
        // 加血逻辑（最多加到 maxHP）
        GameData.Instance.currentHP = Mathf.Min(
            GameData.Instance.currentHP + 20,
            GameData.Instance.maxHP
        );

        // 同步 PlayerStats 状态（如果当前场景中有）
        var stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
        {
            stats.currentHP = GameData.Instance.currentHP;
            stats.UpdateUI();
        }

        Debug.Log($"休息点加血完毕，现在是 {GameData.Instance.currentHP}/{GameData.Instance.maxHP}");

        // 跳转回地图
        SceneManager.LoadScene("Map");
    }

    void UpdateHealText()
    {
        if (healText != null)
        {
            healText.text = "It should be safe here...for now...";
        }
    }
}
