using UnityEngine;
using System.Collections;

public class TextFadeController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;
    public float displayTime = 2.5f;

    void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        float timer = 0f;

        // µ≠»Î
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(displayTime);

        // µ≠≥ˆ
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
