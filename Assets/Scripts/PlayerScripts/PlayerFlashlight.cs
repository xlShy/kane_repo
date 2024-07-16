using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight;

    [SerializeField]
    private float maxBattery;

    [SerializeField]
    public float batteryDrainRate;

    [SerializeField]
    private float flickerThreshold;

    [SerializeField]
    private float flickerIntensity;

    [SerializeField]
    private float flickerFrequency;

    [SerializeField]
    private AudioSource toggleSwitch;

    [SerializeField]
    private GameObject flashlightDustParticles;

    [SerializeField] private float rotationLagSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    private Quaternion targetRotation;
    private Vector3 laggedForward;
    private Vector3 laggedUp;

    private Image dustParticlesImage;
    public float currentBattery;
    private bool isFlickering = false;
    //private float dustParticleFlicker = Random.Range(0f, 1f);
    private float dustParticleFlicker;

    public bool isCanvasEnabled;

    private Vector3 currentLaggedDirection;
    private void OnEnable()
    {
        CanvasManager.OnCanvasEnabled += isAnyCanvasOn;
    }
    private void OnDisable()
    {
        CanvasManager.OnCanvasEnabled -= isAnyCanvasOn;
    }
    private void Start()
    {
        dustParticleFlicker = Random.Range(0f, 1f);
        currentBattery = maxBattery;
        dustParticlesImage = flashlightDustParticles.GetComponent<Image>();
        
        laggedForward = transform.forward;
        laggedUp = transform.up;

        if (playerTransform == null)
        {
            playerTransform = transform.parent;
        }
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        currentLaggedDirection = transform.forward;

    }

    private void Update()
    {
        if (!isCanvasEnabled)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ToggleFlashlight();
                toggleSwitch.Play();
            }

            UpdateFlashlightRotation();

            if (flashlight.enabled)
            {
                DrainBattery();
                CheckForFlicker();
            }
        }
    }

    private void UpdateFlashlightRotation()
    {
        // Always update rotation, even when flashlight is off
        Vector3 targetDirection = cameraTransform.forward;
        currentLaggedDirection = Vector3.Slerp(currentLaggedDirection, targetDirection, rotationLagSpeed * Time.deltaTime);

        // Use LookRotation to create a rotation that looks in the lagged direction
        Quaternion targetRotation = Quaternion.LookRotation(currentLaggedDirection, Vector3.up);

        // Apply the rotation to the flashlight
        transform.rotation = targetRotation;
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
    public void isAnyCanvasOn(bool isOn)
    {
        isCanvasEnabled = isOn;
    }
}
