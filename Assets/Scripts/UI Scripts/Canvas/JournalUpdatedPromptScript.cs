using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JournalUpdatedPromptScript : MonoBehaviour
{
    [SerializeField] private Text textComponent;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayDuration = 2f;

    private void Awake()
    {
        if (textComponent == null)
        {
            Debug.LogError("No TextMeshProUGUI component found on this GameObject.");
        }
    }

    public void ShowPrompt()
    {
        StartCoroutine(FadeInOutCoroutine());
    }

    private IEnumerator FadeInOutCoroutine()
    {
        yield return StartCoroutine(FadeCoroutine(0f, 1f));
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeCoroutine(1f, 0f));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color currentColor = textComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            currentColor.a = newAlpha;
            textComponent.color = currentColor;
            yield return null;
        }

        currentColor.a = endAlpha;
        textComponent.color = currentColor;
    }
}