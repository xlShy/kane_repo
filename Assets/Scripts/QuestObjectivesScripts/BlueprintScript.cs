using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintScript : InteractableObject
{
    [SerializeField] private GameObject blueprintUI;
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            blueprintUI.SetActive(true);
        }
    }
}
