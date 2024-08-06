using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;

    public bool isDoneFading = false;
    void Start()
    {
        //StartCoroutine(FadeInTimer());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutTimer());
        SceneManager.LoadScene(sceneName);
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInTimer());        
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutTimer());
    }
    public IEnumerator FadeInTimer()
    {
        isDoneFading = false;
        float alpha = fadeOutUIImage.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            SetColorAlpha(alpha);
            if(alpha <= 0)
            {
                isDoneFading = true;
            }
            yield return null;
        }
        fadeOutUIImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOutTimer()
    {
        isDoneFading = false;
        fadeOutUIImage.gameObject.SetActive(true);
        float alpha = fadeOutUIImage.color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeSpeed;
            SetColorAlpha(alpha);
            if (alpha >= 1)
            {
                isDoneFading = true;
            }
            yield return null;
        }
    }

    void SetColorAlpha(float alpha)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
    }
}
