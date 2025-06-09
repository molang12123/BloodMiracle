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
        // ��Ѫ�߼������ӵ� maxHP��
        GameData.Instance.currentHP = Mathf.Min(
            GameData.Instance.currentHP + 20,
            GameData.Instance.maxHP
        );

        // ͬ�� PlayerStats ״̬�������ǰ�������У�
        var stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
        {
            stats.currentHP = GameData.Instance.currentHP;
            stats.UpdateUI();
        }

        Debug.Log($"��Ϣ���Ѫ��ϣ������� {GameData.Instance.currentHP}/{GameData.Instance.maxHP}");

        // ��ת�ص�ͼ
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
