using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public InteractableConfig interactableConfig;
    public InventoryItem item;

    private Renderer objectRenderer;
    private bool isRed = false;
    private bool canBeInteracted = true;

    public UnityEvent itemPickedUp;

    private bool isInventoryFull;

    [SerializeField]
    public ChemicalMixingPlace mixingScript;

    private void OnEnable()
    {
        Inventory.onInventoryFull += InventoryFull;
    }
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public virtual void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (canBeInteracted)
        {
            switch (itemInteractedCase)
            {
                case 0:
                    //Debug.Log("Item Interacted With");
                    break;
                case 1:
                    
                    if(item.type == ItemType.Consumables)
                    {
                        if (!isInventoryFull)
                        {
                            itemPickedUp.Invoke();
                            inventory.AddItem(item);
                            canBeInteracted = false;
                        }
                    }
                    else if (item.type == ItemType.KeyItem)
                    {
                        itemPickedUp.Invoke();
                        inventory.AddKeyItem(item);
                        canBeInteracted = false;
                    }
                    break;
                case 2:
                    //Debug.Log("You have interacted with an objective!");
                    break;
                case 3:
                    //Debug.Log("Item Picked Up");
                    if (item != null)
                    {
                        inventory.AddItem(item);
                        canBeInteracted = false;
                    }
                    break;
            }
        }

    }

    public InteractableConfig GetInteractableConfig()
    {
        return interactableConfig;
    }

    public void ResetPuzzle()
    {
        Debug.Log("I reset the puzzle");
        canBeInteracted = true;

    }
    private void InventoryFull(bool isFull)
    {
        isInventoryFull = isFull;
    }
}
