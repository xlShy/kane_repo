using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BathroomDoor : InteractableObject
{
    [SerializeField] private DoorBase doorBase;
    [SerializeField] private ChemicalMixingEventTrigger chemMixTriggerScript;
    [SerializeField] private doorLockerScript doorLockerScript;
    [SerializeField] private ReadableDocumentScript readableDocumentScript;
    [SerializeField] private ChemicalMixingPlace chemicalMixingPlaceScript;
    [SerializeField] private float openSpeed = 5f;
    [SerializeField] private DialogueTriggerScript dialogueTrigger;
    private void Start()
    {
        if (doorLockerScript != null)
        {
            //washroom - chemical puzzle
            doorLockerScript.doorLocked.AddListener(doorBase.LockDoor);
        }
        if (readableDocumentScript != null)
        {
            //office - washroom - safe puzzle
            readableDocumentScript.initiateOfficeEvent.AddListener(canBeMoved);
        }
        if (chemicalMixingPlaceScript != null)
        {
            //washroom - chemical puzzle
            chemicalMixingPlaceScript.OnCompleteChemicalMixing.AddListener(doorBase.UnlockDoor);
        }
        if (dialogueTrigger == null)
        {
            dialogueTrigger = GetComponent<DialogueTriggerScript>();
        }
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (!doorBase.isOpen && doorBase.canOpen)
        {
            doorBase.OpenDoor(openSpeed);
        }
        else if (!doorBase.isOpen && !doorBase.canOpen)
        {
            doorBase.doorIsLocked.Play();

            if (dialogueTrigger != null)
            {
                dialogueTrigger.TriggerDialogue();
            }
        }
        else if (doorBase.isOpen)
        {
            doorBase.CloseDoor(openSpeed);
        }
    }
    private void canBeMoved()
    {
        //Debug.Log("I am called to change the state of the furniture");
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }
}