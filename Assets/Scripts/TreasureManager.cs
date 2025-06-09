using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TreasureManager : MonoBehaviour
{
    public Button claimButton;
    public TextMeshProUGUI rewardText;

    private bool rewardClaimed = false;

    void Start()
    {
        rewardText.text = "Another abandoned box...";

        if (claimButton != null)
        {
            claimButton.onClick.AddListener(ClaimReward);
        }
    }

    void ClaimReward()
    {
        if (rewardClaimed) return;

        int rewardGold = 50;

        // ʹ�� GameData ��ͳһ������
        GameData.Instance.AddGold(rewardGold);

        rewardText.text = $"You picked {rewardGold} gold coins.";
        rewardClaimed = true;

        // �ӳ�1��󷵻ص�ͼ
        Invoke("ReturnToMap", 1f);
    }

    void ReturnToMap()
    {
        SceneManager.LoadScene("Map");
    }
}
