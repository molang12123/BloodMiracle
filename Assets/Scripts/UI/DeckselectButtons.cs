using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSelectButtons : MonoBehaviour
{
    public void OnConfirmClicked()
    {
        //Debug.Log("Deck confirmed!");
        SceneManager.LoadScene("Map"); 
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("Scenes/ClassSelection");
    }
}
