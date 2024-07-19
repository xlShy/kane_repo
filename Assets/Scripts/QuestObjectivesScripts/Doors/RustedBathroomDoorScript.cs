using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RustedBathroomDoor : InteractableObject
{
    [SerializeField] private DoorBase doorBase;

    [SerializeField] private ChemicalMixingEventTrigger chemMixTriggerScript;
    [SerializeField] private doorLockerScript doorLockerScript;
    [SerializeField] private ReadableDocumentScript readableDocumentScript;
    [SerializeField] private ChemicalMixingPlace chemicalMixingPlaceScript;
    [SerializeField] private KeyItemInventory keyItemInventory;

    [SerializeField] private float openSpeed = 5f;

    [SerializeField] private AudioSource deRustingAudioClip;

    private bool isClosing = false;
    private bool isPuzzleCompleted = false;
    private bool isDeRusted = false;


    private void Start()
    {
        if (doorLockerScript != null)
        {
            //washroom - chemical puzzle
            doorLockerScript.doorLocked.AddListener(doorBase.LockDoor);
        }
        if (chemMixTriggerScript != null)
        {
            //washroom - chemical puzzle
            chemMixTriggerScript.chemicalMixingEventInitiate.AddListener(doorBase.LockDoor);
        }

        if (readableDocumentScript != null)
        {
            //office - washroom - safe puzzle
            readableDocumentScript.initiateFlicker.AddListener(canBeMoved);
        }

        if (chemicalMixingPlaceScript != null)
        {
            //washroom - chemical puzzle
            chemicalMixingPlaceScript.puzzleComplete.AddListener(OnPuzzleComplete);
        }
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (!isPuzzleCompleted)
        {
            base.Interact(itemInteractedCase, inventory);
            return;
        }

        if (!isDeRusted)
        {
            InventoryItem deRustingMixture = inventory.GetItemByName("De-Rusting Mixture");
            if (deRustingMixture != null)
            {
                keyItemInventory.RemoveKeyItem(deRustingMixture, 1);
                isDeRusted = true;

                deRustingAudioClip.Play();
                doorBase.UnlockDoor();
            }
        }
        if (!doorBase.isOpen && doorBase.canOpen)
        {
            doorBase.OpenDoor(openSpeed);
        }
        else if (!doorBase.isOpen && !doorBase.canOpen)
        {
            doorBase.doorIsLocked.Play();
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

    private void OnPuzzleComplete()
    {
        isPuzzleCompleted = true;
    }
}