using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object2TriggerDialogueOnly : InteractableObject
{
    [SerializeField] private DialogueTriggerScript dialogue;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        dialogue.TriggerDialogue();
    }
}
