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
    private bool isBreathing = false;

    private void Start()
    {
        postProcessingVolume.profile.TryGet(out vignette);
    }
    private void Update()
    {
        
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
        if (!isBreathing)
        {
            StartCoroutine(BreathingVignetteCoroutine(minIntensity, maxIntensity));
        }
    }
    public void StopBreathingVignette()
    {
        isBreathing = false;
    }

    private IEnumerator BreathingVignetteCoroutine(float minIntensity, float maxIntensity)
    {
        isBreathing = true;
        float time = 0f;

        while (isBreathing)
        {
            time += Time.deltaTime * breathingSpeed;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1) / 2);
            vignette.intensity.value = intensity;

            yield return null;
        }
    }
}