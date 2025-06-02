using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP = 50;
    public int block = 0;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI blockText;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameData.Instance != null);

        if (GameData.Instance.maxHP <= 0)
        {
            Debug.LogWarning("GameData δ���ý�ɫHP����ʹ��Ĭ��ֵ");
            maxHP = 50;
            currentHP = 50;
        }
        else
        {
            maxHP = GameData.Instance.maxHP;
            currentHP = GameData.Instance.currentHP;
        }

        UpdateUI();
        Debug.Log($"PlayerStats ��ʼ����ɣ�{currentHP}/{maxHP}");
    }

    public void GainBlock(int amount)
    {
        block += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (hpText != null)
            hpText.text = $"HP: {currentHP}/{maxHP}";
        if (blockText != null)
            blockText.text = $"Block: {block}";
    }
    public void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - block, 0);
        currentHP -= damageTaken;
        block = Mathf.Max(block - damage, 0);

        // ͬ���� GameData
        GameData.Instance.currentHP = currentHP;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("���������");
        }

        UpdateUI();
    }
}
