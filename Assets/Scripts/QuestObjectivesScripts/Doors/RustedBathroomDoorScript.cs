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
    [SerializeField] private AudioSource splashMixtureAudio;
    [SerializeField] private GameObject deRustBucket;
    [SerializeField] private PuzzleEventHandler pEventHandler;
    [SerializeField] private Puzzle puzzle;

    private bool isPuzzleCompleted = false;
    private bool isDeRusted = false;

    private void Start()
    {
        if (chemicalMixingPlaceScript != null)
        {
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
                if (splashMixtureAudio != null)
                {
                    splashMixtureAudio.Play();
                }

                if (mugWithMixture.isCorrectMixture)
                {
                    inventory.ConvertFilledMugToEmpty(mugWithMixture);
                    isDeRusted = true;
                    deRustingAudioClip.Play();
                    doorBase.UnlockDoor();
                    if (pEventHandler != null && puzzle != null)
                    {
                        pEventHandler.InteractPuzzle(puzzle);
                    }
                    if (deRustBucket != null)
                    {
                        deRustBucket.SetActive(true);
                    }
                    Debug.Log("Door has been de-rusted and unlocked!");
                }
                else
                {
                    inventory.ResetMugToEmpty(mugWithMixture);
                    Debug.Log("The mixture is incorrect. The mug has been emptied.");
                }
            }
        }
        else
        {
            TryOpenCloseDoor();
        }
    }

    private void TryOpenCloseDoor()
    {
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