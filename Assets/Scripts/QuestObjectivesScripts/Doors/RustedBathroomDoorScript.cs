using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [SerializeField] private DialogueTriggerScript dialogueTrigger;
    [SerializeField] private DialogueTriggerScript doorDerustedDialogueTrigger;
    [SerializeField] private DialogueTriggerScript doorStillRustedDialogueTrigger;

    private bool isPuzzleCompleted = false;
    private bool isDeRusted = false;

    public UnityEvent doorUnrusted;

    private void Start()
    {
        if (chemicalMixingPlaceScript != null)
        {
            chemicalMixingPlaceScript.OnCompleteChemicalMixing.AddListener(OnPuzzleComplete);
        }

        if (dialogueTrigger == null)
        {
            dialogueTrigger = GetComponent<DialogueTriggerScript>();
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

        InventoryItem mugWithMixture = inventory.GetKeyItem("Mug with Mixture");

        if (mugWithMixture != null && !isDeRusted)
        {
            Debug.Log("The derust event is beiong called!");
            if (splashMixtureAudio != null)
            {
                splashMixtureAudio.Play();
            }
            if (mugWithMixture.isCorrectMixture)
            {
                inventory.ConvertFilledMugToEmpty(mugWithMixture);
                isDeRusted = true;
                isPuzzleCompleted = true;
                deRustingAudioClip.Play();
                doorBase.UnlockDoor();
                if (pEventHandler != null && puzzle != null)
                {
                    pEventHandler.InteractPuzzle(puzzle);
                }
                Debug.Log("Door has been de-rusted and unlocked!");
                doorUnrusted.Invoke();
                doorDerustedDialogueTrigger.TriggerDialogue();
            }
            else
            {
                inventory.ResetMugToEmpty(mugWithMixture);
                Debug.Log("The mixture is incorrect. The mug has been emptied.");
                doorStillRustedDialogueTrigger.TriggerDialogue();
            }
        }
        else if (isDeRusted)
        {
            TryOpenCloseDoor();
        }
        else if (!doorBase.canOpen)
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
            TriggerLockedDoorDialogue();
        }
        else if (doorBase.isOpen)
        {
            doorBase.CloseDoor(openSpeed);
        }
    }

    private void TriggerLockedDoorDialogue()
    {
        if (dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
        else
        {
            Debug.Log("Not assigned.");
        }
    }
    private void OnPuzzleComplete()
    {
        isPuzzleCompleted = true;
        Debug.Log("Chemical Mixing Puzzle Completion!");
    }
}