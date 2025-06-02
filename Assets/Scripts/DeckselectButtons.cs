using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSelectButtons : MonoBehaviour
{
    public void OnConfirmClicked()
    {
        //Debug.Log("Deck confirmed!");
        SceneManager.LoadScene("Map"); // 替换为你地图场景的实际名字
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("Scenes/ClassSelection");
    }
}
