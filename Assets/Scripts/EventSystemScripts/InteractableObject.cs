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
            case 0:
                //Debug.Log("Item Interacted With");
                break;
            case 1:

                if (item.type == ItemType.Consumables)
                {
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
                Debug.Log("Item Picked Up");
                pickUpSound.Play();
                if (item != null)
                {
                    inventory.AddItem(item);
                }
                break;
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
}
