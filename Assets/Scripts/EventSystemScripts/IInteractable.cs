using UnityEngine;

public interface IInteractable
{
    void Interact(int itemInteractedCase, Inventory inventory);
    InteractableConfig GetInteractableConfig();
}
