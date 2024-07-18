using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ReadableDocumentScript : InteractableObject
{
    [SerializeField] private GameObject readableDocumentUI;

    [SerializeField] private InterfaceOnClose interfaceOnCloseScript;

    public UnityEvent initiateFlicker;
    private void Start()
    {
        interfaceOnCloseScript.interfaceClosed.AddListener(initiateFlickerEvent);
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            readableDocumentUI.SetActive(true);
        }
    }

    private void initiateFlickerEvent()
    {
        initiateFlicker.Invoke();
    }
}
