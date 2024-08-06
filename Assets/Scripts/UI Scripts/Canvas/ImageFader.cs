using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFader : MonoBehaviour
{
    public Image imageToFade;
    public float fadeDuration = 1f;

    private bool isFading = false;

    [SerializeField] private AudioSource keybindsAppearSoundFX;

    private void Start()
    {
        Color initialColor = imageToFade.color;
        initialColor.a = 0f;
        imageToFade.color = initialColor;
    }

    public void StartFadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeImageCoroutine(0f, 1f));
        }
    }

    private IEnumerator FadeImageCoroutine(float startAlpha, float endAlpha)
    {
        keybindsAppearSoundFX.Play();
        isFading = true;
        float elapsedTime = 0f;
        Color currentColor = imageToFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            currentColor.a = newAlpha;
            imageToFade.color = currentColor;
            yield return null;
        }

        currentColor.a = endAlpha;
        imageToFade.color = currentColor;
        isFading = false;
    }
}