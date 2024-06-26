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
                    Debug.Log("Item Interacted With");
                    break;
                case 1:
                    Debug.Log("Item Picked Up case 1");
                    itemPickedUp.Invoke();
                    inventory.AddItem(item);
                    break;
                case 2:
                    Debug.Log("You have interacted with an objective!");
                    break;
                case 3:
                    Debug.Log("Item Picked Up");
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
}
