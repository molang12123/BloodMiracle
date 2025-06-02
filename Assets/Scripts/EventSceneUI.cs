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
        // ���޸� currentNodeIndex����һص�ͼ��ӵ�ǰ�ڵ�ѡ��ǰ��
        SceneManager.LoadScene("Map");
    }
}
