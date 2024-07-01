using UnityEngine;

public enum InteractableType
{
    Interactable,
    PickUp,
    Objective,
    PickupnoDestroy
}

[CreateAssetMenu(menuName = "Interactables/interactables")]
public class InteractableConfig : ScriptableObject
{
    public Sprite promptImage;
    public InteractableType interactableType;
}