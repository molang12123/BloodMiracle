using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("ClassSelection");
    }

    public void OnExitButton()
    {
        Application.Quit();
        Debug.Log("Quit the game already(the compilor stop running)");
    }
}
