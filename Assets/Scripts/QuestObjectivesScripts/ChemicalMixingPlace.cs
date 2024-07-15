using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChemicalMixingPlace : InteractableObject
{
    private Renderer objectRenderer;

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    public ChemicalMixingEventTrigger eventScript;

    [SerializeField] public Inventory inventory;
    [SerializeField] public KeyItemInventory keyInventory;

    [SerializeField] private string[] requiredItems = { "Dish Soap", "Baking Soda" };
    [SerializeField] private string[] failItems = { "Salt", "Pepper" };

    public UnityEvent resetInteractableState;
    public UnityEvent puzzleComplete;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        eventScript.chemicalMixingEventInitiate.AddListener(StartPuzzle);
    }

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        bool hasAllRequiredItems = true;
        bool hasFailItem = false;
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();

        // Check for required items
        foreach (string itemName in requiredItems)
        {
            InventoryItem item = inventory.GetItemByName(itemName);
            if (item != null)
            {
                itemsToRemove.Add(item);
            }
            else
            {
                hasAllRequiredItems = false;
            }
        }

        // Check for fail items
        foreach (string failItemName in failItems)
        {
            InventoryItem failItem = inventory.GetItemByName(failItemName);
            if (failItem != null)
            {
                hasFailItem = true;
                itemsToRemove.Add(failItem);
            }
        }

        // Remove all found items from the key item inventory
        foreach (var item in itemsToRemove)
        {
            keyInventory.RemoveKeyItem(item, 1);
            inventory.RemoveKeyItem(item);
        }

        // Check puzzle state
        if (hasAllRequiredItems && !hasFailItem)
        {
            Debug.Log("Puzzle Solved!");
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
            puzzleComplete.Invoke();
        }
        else
        {
            Debug.Log("Fail!");
            if (hasFailItem)
            {
                Debug.Log("Incorrect!");
            }
            else
            {
                Debug.Log("Kulang!");
            }
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
        resetInteractableState.Invoke();
    }
    
    private void StartPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
        Debug.Log("Can mix in the place now!");
    }
}
