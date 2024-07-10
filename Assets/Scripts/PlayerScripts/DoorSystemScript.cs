using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DoorSystemScript : InteractableObject
{
    private bool canOpen = true;
    private bool isOpen = false;
    [SerializeField] private ChemicalMixingEventTrigger chemMixTriggerScript;

    [SerializeField] private float openSpeed = 5f;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private void Start()
    {
        chemMixTriggerScript.chemicalMixingEventInitiate.AddListener(chemicalTrigger);
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, 90, 0);
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && !isOpen && canOpen)
        {
            StartCoroutine(OpenDoor());
        }
        else if (isOpen)
        {
            StartCoroutine(CloseDoor());
        }
    }
    private IEnumerator OpenDoor()
    {
        while (Quaternion.Angle(transform.rotation, openRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = openRotation;
        isOpen = true;
    }

    private IEnumerator CloseDoor()
    {
        while (Quaternion.Angle(transform.rotation, closedRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = closedRotation;
        isOpen = false;
    }
    private void chemicalTrigger()
    {
        canOpen = false;
    }
}
