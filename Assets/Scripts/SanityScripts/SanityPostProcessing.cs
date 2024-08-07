using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityPostProcessing : MonoBehaviour
{
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private bool disable;

    [Header("Post Processing Profile")]
    [SerializeField] private VolumeProfile sanityProfile;

    [Header("Post Processing Effects")]
    private Vignette vignette;
    public float lastVignetteValue;
    public float currentIntensity;
    public bool hasAdjusted = false;

    [Header("Breathing Vignette Settings")]
    [SerializeField] private float breathingCycleDuration = 2f;

    Coroutine vignetteCoroutine;

    private void Start()
    {
        postProcessingVolume.profile.TryGet(out vignette);
    }
    private void Update()
    {
        //print(vignette.intensity.value);
    }
    public void MainPostProcess()
    {
        postProcessingVolume.profile = sanityProfile;
    }
    public void DisablePostProcess()
    {
        disable = !disable;
        postProcessingVolume.enabled = disable;
    }
    public void StartSanityEffect(float minIntensity, float maxIntensity)
    {
        if (vignetteCoroutine != null)
        {
            StopCoroutine(vignetteCoroutine);
        }
        hasAdjusted = false;
        lastVignetteValue = currentIntensity;
        vignetteCoroutine = StartCoroutine(BreathingVignetteCoroutine(minIntensity, maxIntensity));
    }
    public void StopBreathingVignette()
    {
        if (vignetteCoroutine != null)
        {
            StopCoroutine(vignetteCoroutine);
            vignetteCoroutine = null;
        }
        // Reset the vignette intensity to 0 or any default value
        vignette.intensity.value = 0f;
    }

    private IEnumerator BreathingVignetteCoroutine(float minIntensity, float maxIntensity)
    {
        float elapsedTime = 0f;
        float adjustmentDuration = 1f; // Time to transition to new intensity
        float adjustmentProgress = 0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            

            if (!hasAdjusted)
            {
                adjustmentProgress += Time.deltaTime / adjustmentDuration;
                currentIntensity = Mathf.Lerp(lastVignetteValue, maxIntensity, adjustmentProgress);

                if (adjustmentProgress >= 1f)
                {
                    hasAdjusted = true;
                    adjustmentProgress = 0f;
                }
            }
            else
            {
                float t = elapsedTime / breathingCycleDuration;
                float breathingProgress = Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f;
                currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, breathingProgress);         
            }
            vignette.intensity.Override(currentIntensity);

            if (elapsedTime >= breathingCycleDuration)
            {
                elapsedTime = 0f;
            }

            yield return null;
        }
    }
}