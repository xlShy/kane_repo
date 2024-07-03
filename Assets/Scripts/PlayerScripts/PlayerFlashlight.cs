using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    private Light flashlight;

    [SerializeField]
    private float maxBattery;

    [SerializeField]
    private float batteryDrainRate;

    [SerializeField]
    private float flickerThreshold;

    [SerializeField]
    private float flickerIntensity;

    [SerializeField]
    private float flickerFrequency;

    [SerializeField]
    private AudioSource toggleSwitch;
    public float currentBattery;
    private bool isFlickering = false;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        currentBattery = maxBattery;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ToggleFlashlight();
            toggleSwitch.Play();
        }

        if (flashlight.enabled)
        {
            DrainBattery();
            CheckForFlicker();
        }
    }

    private void ToggleFlashlight()
    {
        if (!flashlight.enabled && currentBattery>0)
        {
            flashlight.enabled = true;
            if (currentBattery <= flickerThreshold && !isFlickering)
            {
                StartCoroutine(FlickerLight());
            }
        }
        else
        {
            flashlight.enabled = false;
            StopAllCoroutines();
            isFlickering = false;
        }
    }

    private void DrainBattery()
    {
        currentBattery -= batteryDrainRate * Time.deltaTime;
        if (currentBattery <= 0)
        {
            currentBattery = 0;
            flashlight.enabled = false;
            StopAllCoroutines();
            isFlickering = false;
        }
    }

    private void CheckForFlicker()
    {
        if (currentBattery <= flickerThreshold && !isFlickering)
        {
            StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight()
    {
        isFlickering = true;
        float originalIntensity = flashlight.intensity;

        while (flashlight.enabled && currentBattery <= flickerThreshold)
        {
            flashlight.intensity = Random.Range(originalIntensity - flickerIntensity, originalIntensity);
            yield return new WaitForSeconds(flickerFrequency);
        }

        flashlight.intensity = originalIntensity;
        isFlickering = false;
    }
}
