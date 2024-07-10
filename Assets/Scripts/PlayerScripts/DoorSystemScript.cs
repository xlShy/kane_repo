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

    private void Start()
    {
        chemMixTriggerScript.chemicalMixingEventInitiate.AddListener(chemicalTrigger);
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && !isOpen && canOpen)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            isOpen = true;
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isOpen = false;
        }
    }

    private void chemicalTrigger()
    {
        canOpen = false;
    }
}
