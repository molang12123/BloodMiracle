using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAutoSkip : MonoBehaviour
{
    void Start()
    {
        Invoke("GoToNextScene", 5f);
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
