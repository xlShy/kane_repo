using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static LightManager;

public class LightManager : MonoBehaviour, ISwitchable
{
    [Header("Insert Light Object Parent To Flicker")]
    //[SerializeField] private List<GameObject> gameObjectsWithLights;
    [SerializeField] private List<Light> managedLights;
    [SerializeField] private List<Light> selectedLights;
    [SerializeField] private ReadableDocumentScript readableDocumentScript;
    [SerializeField] private ChemicalMixingPlace chemicalMixingPlaceScript;

    [Header("Audio Settings")]
    [SerializeField] private List<AudioClip> audioSequence;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float audioDelay = 0.5f; // New variable for audio delay

    private Coroutine flickerCoroutine;
    private Coroutine audioCoroutine;

    public UnityEvent OnDisableLightsOnStart;
    //for testing
    private bool isLightOn;

    private void Start()
    {
        //InitializeManagedLights();
        OnDisableLightsOnStart?.Invoke();
    }
    //test purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isLightOn = !isLightOn;
            if (isLightOn)
            {
                EnableLightFlicker();
                print("enable flicker");
            }
            else
            {
                DisableLightFlicker();
            }
        }
    }
    public void Toggle(bool toggleStatus)
    {
        foreach (var light in managedLights)
        {
            light.enabled = toggleStatus;

            if (toggleStatus)
            {
                MeshRenderer renderer = light.GetComponentInParent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.EnableKeyword("_EMISSION");
                }
            }
            else if (!toggleStatus)
            {
                MeshRenderer renderer = light.GetComponentInParent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
    public void TurnOnAll()
    {
        Toggle(true);

    }

    public void TurnOffAll()
    {
        Toggle(false);
    }
    public void EnableGenerator()
    {
        StartCoroutine(GeneratorLights());
    }
    public void EnableLightFlicker()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(FlickerLights());
        }
        if (audioCoroutine == null)
        {
            audioCoroutine = StartCoroutine(PlayAudioSequence());
        }
    }
    public void DisableLightFlicker()
    {
        if (flickerCoroutine != null)
        {
            selectedLights.Clear();
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
            // Ensure all lights are turned on after stopping the flicker
            TurnOnAll();
        }
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
            audioCoroutine = null;
            audioSource.Stop();
        }
    }
    public void InitializeManagedLights(LightDataStorer gameObjectsWithLights)
    {
        managedLights = new List<Light>();
        foreach (var gameObject in gameObjectsWithLights.lights2Initialize)
        {
            Light[] lights = gameObject.GetComponentsInChildren<Light>();
            managedLights.AddRange(lights);
        }
    }
    private void SelectLightToFlicker()
    {
        if (managedLights.Count == 0) return;
        int selectedLightsNumber = Random.Range(0, managedLights.Count);
        Light selectedLight = managedLights[selectedLightsNumber];
        if (!selectedLights.Contains(selectedLight))
        {
            selectedLights.Add(selectedLight);
        }
    }
    private IEnumerator GeneratorLights()
    {
        if (flickerCoroutine == null && audioCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(FlickerLights());
        }

        yield return new WaitForSeconds(3f);

        DisableLightFlicker();
        TurnOnAll();
    }
    private IEnumerator FlickerLights()
    {
        while (true)
        {
            SelectLightToFlicker();
            foreach (var light in selectedLights)
            {
                light.enabled = !light.enabled;
                MeshRenderer renderer = light.GetComponentInParent<MeshRenderer>();
                if (light.enabled)
                {
                    
                    if (renderer != null)
                    {
                        renderer.material.EnableKeyword("_EMISSION");
                    }
                }
                else if (!light.enabled)
                {
                    if (renderer != null)
                    {
                        renderer.material.DisableKeyword("_EMISSION");
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        }
    }
    private IEnumerator PlayAudioSequence()
    {
        while (true)
        {
            foreach (var clip in audioSequence)
            {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length + audioDelay); // Added delay
            }
        }
    }
}