using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LightManager;

public class LightManager : MonoBehaviour, ISwitchable
{
    [Header("Insert Light Object Parent To Flicker")]
    [SerializeField] private List<GameObject> gameObjectsWithLights;

    [SerializeField] private List<Light> managedLights;
    [SerializeField] private List<Light> selectedLights;

    private Coroutine flickerCoroutine;

    //for testing
    private bool isLightOn;

    private void Start()
    {
        InitializeManagedLights();
        TurnOffAll();
    }

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
    public void EnableLightFlicker()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(FlickerLights());
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
    }
    private void InitializeManagedLights()
    {
        managedLights = new List<Light>();

        foreach (var gameObject in gameObjectsWithLights)
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
    private IEnumerator FlickerLights()
    {
        while (true)
        {
            SelectLightToFlicker();
            foreach (var light in selectedLights)
            {
                light.enabled = !light.enabled;
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}
