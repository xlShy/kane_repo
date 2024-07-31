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

    private bool isPuzzleCompleted = false;
    private bool isDeRusted = false;


    private void Start()
    {
        //doorBase.LockDoor();
        if (doorLockerScript != null)
        {
            //washroom - chemical puzzle
            //doorLockerScript.doorLocked.AddListener(doorBase.LockDoor);
        }
        if (chemicalMixingPlaceScript != null)
        {
            //washroom - chemical puzzle
            chemicalMixingPlaceScript.OnCompleteChemicalMixing.AddListener(OnPuzzleComplete);
        }
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        Debug.Log("I am being activated.");
        Debug.Log($"isPuzzleCompleted: {isPuzzleCompleted}, isDeRusted: {isDeRusted}");

        if (!isPuzzleCompleted)
        {
            base.Interact(itemInteractedCase, inventory);
        }

        if (!isDeRusted)
        {
            Debug.Log("The derust event is being called!");
            InventoryItem mugWithMixture = inventory.GetKeyItem("Mug with Mixture");
            if (mugWithMixture != null)
            {
                if (mugWithMixture.isCorrectMixture)
                {
                    inventory.ConvertFilledMugToEmpty(mugWithMixture);
                    isDeRusted = true;
                    deRustingAudioClip.Play();
                    doorBase.UnlockDoor();
                    Debug.Log("Door has been de-rusted and unlocked!");
                }
                else
                {
                    inventory.ResetMugToEmpty(mugWithMixture);
                    Debug.Log("The mixture is incorrect. The mug has been emptied.");
                }
            }
            else
            {
                Debug.Log("You need a mixture to de-rust this door.");
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
    private void OnPuzzleComplete()
    {
        isPuzzleCompleted = true;
    }
}