using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float alpha = fadeOutUIImage.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            SetColorAlpha(alpha);
            yield return null;
        }
        fadeOutUIImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeOutUIImage.gameObject.SetActive(true);
        float alpha = fadeOutUIImage.color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeSpeed;
            SetColorAlpha(alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    void SetColorAlpha(float alpha)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
    }
}
