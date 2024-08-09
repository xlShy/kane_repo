using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenEnabler : MonoBehaviour
{
    [SerializeField] public GameObject deathScreenCanvas;
    [SerializeField] private SceneFader sceneFader;

    private Image screenImage;

    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float stayDuration = 2f;
    [SerializeField] private float fadeOutDuration = 1f;
    public bool isDoneShowing;

    private void Start()
    {
        screenImage = deathScreenCanvas.GetComponentInChildren<Image>();
    }
    
    public void EnableDeathScreen()
    {
        StartCoroutine(ScreenEnabler());       
    }

    private IEnumerator ScreenEnabler()
    {
        if (!isDoneShowing)
        {
            deathScreenCanvas.SetActive(true);
            //Debug.Log("Enabling death screen canvas");
            yield return StartCoroutine(FadeImage(0f, 1f, fadeInDuration));

            yield return new WaitForSeconds(stayDuration);  

            //Debug.Log("Disabling death screen canvas");
            yield return StartCoroutine(FadeImage(1f, 0f, fadeOutDuration));

            deathScreenCanvas.SetActive(false);

            yield return new WaitUntil(() => !deathScreenCanvas.activeSelf);
        }
    }
    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color currentColor = screenImage.color;
        print("Fade");
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            screenImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;           
        }
        //screenImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, endAlpha);        
    }
}
