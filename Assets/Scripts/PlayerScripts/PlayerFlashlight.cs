using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private InteractionHandler interactionHandler;

    [SerializeField]
    private GameObject flashlightDustParticles;

    private Image dustParticlesImage;
    public float currentBattery;
    private bool isFlickering = false;
    private float dustParticleFlicker = Random.Range(0f, 1f);

    private void Start()
    {
        flashlight = GetComponent<Light>();
        currentBattery = maxBattery;
        dustParticlesImage = flashlightDustParticles.GetComponent<Image>();
    }

    private void Update()
    {
        if (!interactionHandler.IsAnyCanvasActive())
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
        
    }

    private void ToggleFlashlight()
    {
        if (!flashlight.enabled && currentBattery>0)
        {
            flashlight.enabled = true;
            flashlightDustParticles.SetActive(true);
            if (currentBattery <= flickerThreshold && !isFlickering)
            {
                StartCoroutine(FlickerLight());
            }
        }
        else
        {
            flashlight.enabled = false;
            flashlightDustParticles.SetActive(false);
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
        Color originalDustColor = dustParticlesImage.color;

        while (flashlight.enabled && currentBattery <= flickerThreshold)
        {
            flashlight.intensity = Random.Range(originalIntensity - flickerIntensity, originalIntensity);

            Color newColor = dustParticlesImage.color;
            newColor.a = originalDustColor.a * dustParticleFlicker;
            dustParticlesImage.color = newColor;
            yield return new WaitForSeconds(flickerFrequency);   
        }

        flashlight.intensity = originalIntensity;
        isFlickering = false;
    }
}
