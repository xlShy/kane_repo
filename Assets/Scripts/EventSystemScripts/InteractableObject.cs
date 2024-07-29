using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public InteractableConfig interactableConfig;
    public InventoryItem item;

    public UnityEvent itemPickedUp;

    private bool isInventoryFull;

    public ChemicalMixingPlace mixingScript;

    [SerializeField]
    private AudioSource pickUpSound;

    private void OnEnable()
    {
        Inventory.onInventoryFull += InventoryFull;
    }

    public virtual void Interact(int itemInteractedCase, Inventory inventory)
    {
        switch (itemInteractedCase)
        {
            case 1:
                Debug.Log("Case 1 is called");
                if (item.type == ItemType.Consumables)
                {
                    Debug.Log("This is a consumable");
                    if (!isInventoryFull)
                    {
                        itemPickedUp.Invoke();
                        inventory.AddItem(item);
                    }
                }
                else if (item.type == ItemType.KeyItem)
                {
                    itemPickedUp.Invoke();
                    inventory.AddKeyItem(item);
                }
                break;
            case 2:
                //Debug.Log("You have interacted with an objective!");
                break;
            case 3:
                Debug.Log("Case 3: Handling Chemical Ingredient");
                if (IsChemicalIngredient(item))
                {
                    HandleIngredientInteraction(inventory);
                }
                else
                {
                    HandleGeneralItemPickup(inventory);
                }
                break;
        }
    }
    private bool IsChemicalIngredient(InventoryItem item)
    {
        bool isChemical = gameObject.CompareTag("Chemical");
        Debug.Log($"Is Chemical Ingredient: {isChemical}");
        return isChemical;
    }

    private void HandleGeneralItemPickup(Inventory inventory)
    {
        Debug.Log("Item Picked Up");
        PlayPickupSound();
        if (item!= null)
        {
            inventory.AddItem(item);
        }
    }

    private void PlayPickupSound()
    {
        if (pickUpSound != null)
        {
            pickUpSound.Play();
        }
    }
    public InteractableConfig GetInteractableConfig()
    {
        return interactableConfig;
    }

    private void InventoryFull(bool isFull)
    {
        isInventoryFull = isFull;
    }

    private void HandleIngredientInteraction(Inventory inventory)
    {
        Debug.Log("Handling Ingredient Interaction now.");
        if (inventory.HasEmptyMug())
        {
            inventory.AddIngredientToMug(item);
            itemPickedUp.Invoke();
            PlayPickupSound();
            Debug.Log($"Added {item.itemName} to Mug");
        }
        else if (inventory.HasFilledMug())
        {
            Debug.Log("Mug already contains an ingredient");
        }
        else
        {
            Debug.Log("You need a Mug to interact with ingredients");
        }
    }
}
