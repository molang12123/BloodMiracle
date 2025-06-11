using UnityEngine;
using System.Collections;

public class FadeInCanvas : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f; // 淡入时间，单位秒

    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // 保证完全显示
    }
}
