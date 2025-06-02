using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventSceneUI : MonoBehaviour
{
    public Button returnButton;

    void Start()
    {
        returnButton.onClick.AddListener(HandleReturnToMap);
    }

    void HandleReturnToMap()
    {
        // 不修改 currentNodeIndex，玩家回地图后从当前节点选择前进
        SceneManager.LoadScene("Map");
    }
}
