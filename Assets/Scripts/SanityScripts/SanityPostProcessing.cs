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

    [SerializeField] private float breathingSpeed = 2f;

    Coroutine vignetteCoroutine;

    private void Start()
    {
        postProcessingVolume.profile.TryGet(out vignette);
    }
    private void Update()
    {
        print(vignette.intensity.value);
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
        Debug.Log("ISCALLED");
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime * breathingSpeed;
            float normalizedT = (Mathf.Sin(t * Mathf.PI) + 1f) / 2f;
            float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedT);
            vignette.intensity.Override(currentIntensity);
            yield return null;
        }
    }
}