using UnityEngine;
using System.Collections;

public class CameraFlash : MonoBehaviour
{
    public Light flashLight;
    public float flashDuration = 0.1f;
    public float maxIntensity = 5f;
    public float defaultDelay = 0.5f; // Default delay time

    [SerializeField] public AudioSource cameraShutter;

    public void TriggerFlash(float delay = -1f)
    {
        StartCoroutine(DelayedFlashRoutine(delay));
    }

    private IEnumerator DelayedFlashRoutine(float delay)
    {
        // Use the provided delay, or the default if no delay was specified
        float actualDelay = delay >= 0 ? delay : defaultDelay;

        // Wait for the specified delay
        yield return new WaitForSeconds(actualDelay);

        // Then start the flash
        yield return StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        cameraShutter.Play();
        flashLight.intensity = maxIntensity;
        yield return new WaitForSeconds(flashDuration);
        flashLight.intensity = 0f;
    }
}