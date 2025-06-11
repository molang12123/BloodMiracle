using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SimpleSubtitleFade : MonoBehaviour
{
    public TMP_Text line1;
    public TMP_Text line2;

    public float fadeDuration = 1.5f;
    public float stayDuration = 2f;
    public string mainMenuSceneName = "MainMenu"; // ����Ϊ������˵�������

    void Start()
    {
        SetAlpha(line1, 0f);
        SetAlpha(line2, 0f);

        StartCoroutine(PlaySubtitleSequence());
    }

    IEnumerator PlaySubtitleSequence()
    {
        // ��1����Ļ���� �� ͣ�� �� ����
        yield return StartCoroutine(FadeText(line1, 0f, 1f));
        yield return new WaitForSeconds(stayDuration);
        yield return StartCoroutine(FadeText(line1, 1f, 0f));

        // ��2����Ļ���� �� ͣ�� �� ����
        yield return StartCoroutine(FadeText(line2, 0f, 1f));
        yield return new WaitForSeconds(stayDuration);
        yield return StartCoroutine(FadeText(line2, 1f, 0f));

        // �� 0.5 �����л�����
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    IEnumerator FadeText(TMP_Text text, float fromAlpha, float toAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / fadeDuration);
            SetAlpha(text, alpha);
            yield return null;
        }
        SetAlpha(text, toAlpha);
    }

    void SetAlpha(TMP_Text text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }
}
