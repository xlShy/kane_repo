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
    [SerializeField] private float breathingCycleDuration = 2f;

    [Header("Vignette Settings")]
    private Vignette vignette;
    public float lastVignetteValue;
    public float currentIntensity;
    public bool hasAdjusted = false;

    [Header("Chromatic Abberation Settings")]
    private ChromaticAberration chromaticAbberation;
    public bool isChromaticOn;

    [Header("Lens Distortion Settings")]
    private LensDistortion lensDistortion;
    [SerializeField] private float minDistortionX = 0.1f;
    [SerializeField] private float maxDistortionX = 0.5f;
    [SerializeField] private float minDistortionY = 0.1f;
    [SerializeField] private float maxDistortionY = 0.5f;
    
    [SerializeField] private float xCycleOffset = 0.5f;

    [Header("Depth Of Field")]
    private DepthOfField depthOfField;
    [SerializeField] private float minFocalLength = 1f;
    [SerializeField] private float maxFocalLength = 10f;

    Coroutine vignetteCoroutine;

    private void Start()
    {
        postProcessingVolume.profile.TryGet(out vignette);
        postProcessingVolume.profile.TryGet(out chromaticAbberation);
        postProcessingVolume.profile.TryGet(out lensDistortion);
        postProcessingVolume.profile.TryGet(out depthOfField);
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
    public void StartVignetteEffect(float minIntensity, float maxIntensity)
    {
        if (vignetteCoroutine != null)
        {
            StopCoroutine(vignetteCoroutine);
        }
        hasAdjusted = false;
        lastVignetteValue = currentIntensity;

        vignetteCoroutine = StartCoroutine(BreathingVignetteCoroutine(minIntensity, maxIntensity));
    }
    public void StartLensDistortionEffect()
    {
        StartCoroutine(BreathingLensDistortionCoroutine());
    }
    public void ToggleChomaticAbberation(bool isOn)
    {
        chromaticAbberation.active = isOn;
        isChromaticOn = isOn;
    }
    public void StartDepthOfFieldEffect()
    {
        StartCoroutine(BreathingDepthOfFieldCoroutine());
    }
    #region VignetteSetup
    public void StopVignetteEffect()
    {
        if (vignetteCoroutine != null)
        {
            StopCoroutine(vignetteCoroutine);
            vignetteCoroutine = null;
        }
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
    #endregion

    #region LensDistortionSetup
    private IEnumerator BreathingLensDistortionCoroutine()
    {
        while (true)
        {
            // Generate random start and end values for this cycle
            float startX = Random.Range(minDistortionX, maxDistortionX);
            float endX = Random.Range(minDistortionX, maxDistortionX);
            float startY = Random.Range(minDistortionY, maxDistortionY);
            float endY = Random.Range(minDistortionY, maxDistortionY);

            float elapsedTime = 0f;
            while (elapsedTime < breathingCycleDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / breathingCycleDuration;

                // Calculate X distortion with offset
                float breathingProgressX = Mathf.Sin((t + xCycleOffset) * Mathf.PI * 2) * 0.5f + 0.5f;
                float currentIntensityX = Mathf.Lerp(startX, endX, breathingProgressX);

                // Calculate Y distortion
                float breathingProgressY = Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f;
                float currentIntensityY = Mathf.Lerp(startY, endY, breathingProgressY);

                // Apply distortion
                lensDistortion.intensity.Override(currentIntensityX);
                lensDistortion.xMultiplier.Override(currentIntensityX);
                lensDistortion.yMultiplier.Override(currentIntensityY);

                yield return null;
            }
        }
    }

    public void StopLensDistortionEffect()
    {
        StopAllCoroutines();
        lensDistortion.intensity.Override(0f);
        lensDistortion.xMultiplier.Override(1f);
        lensDistortion.yMultiplier.Override(1f);
    }
    #endregion

    #region DepthOfFieldSetup
    public void StopDepthOfFieldEffect()
    {
        StopCoroutine(nameof(BreathingDepthOfFieldCoroutine));
        depthOfField.focalLength.Override(1f); 
    }
    private IEnumerator BreathingDepthOfFieldCoroutine()
    {
        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < breathingCycleDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / breathingCycleDuration;

                // Calculate the breathing effect using a sine wave
                float breathingProgress = Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f;
                float currentFocalLength = Mathf.Lerp(minFocalLength, maxFocalLength, breathingProgress);

                // Apply the focal length
                depthOfField.focalLength.Override(currentFocalLength);

                yield return null;
            }
        }
    }
    #endregion
}