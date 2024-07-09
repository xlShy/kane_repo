using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableDocumentScript : InteractableObject
{
    [SerializeField] private GameObject readableDocumentUI;
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            readableDocumentUI.SetActive(true);
        }
    }
}
