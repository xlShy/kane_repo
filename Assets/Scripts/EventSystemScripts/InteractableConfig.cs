using UnityEngine;

public enum InteractableType
{
    Interactable,
    Pickup,
    Objective,
    PickupnoDestroy
}

[CreateAssetMenu(menuName = "Interactables/interactables")]
public class InteractableConfig : ScriptableObject
{
    public Sprite promptImage;
    public InteractableType interactableType;
}