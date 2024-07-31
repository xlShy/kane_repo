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
        Debug.Log("Breathing Vignette Started");
        float elapsedTime = 0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / breathingCycleDuration;

            // Use a sine wave to create a smooth breathing effect
            float breathingProgress = Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f;
            float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, breathingProgress);

            vignette.intensity.Override(currentIntensity);

            // Reset elapsed time when a full cycle is complete
            if (elapsedTime >= breathingCycleDuration)
            {
                elapsedTime = 0f;
            }

            yield return null;
        }
    }
}