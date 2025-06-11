using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFadeOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public string nextSceneName = "ClassSelection";

    public void StartGame()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        // 开始淡出
        float time = 0f;
        float startAlpha = canvasGroup.alpha;

        // 禁止点击
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;

        // 场景切换
        SceneManager.LoadScene(nextSceneName);
    }
}
