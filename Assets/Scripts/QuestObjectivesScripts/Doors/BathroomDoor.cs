using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BathroomDoor : InteractableObject
{
    [SerializeField] private DoorBase doorBase;

    [SerializeField] private ChemicalMixingEventTrigger chemMixTriggerScript;
    [SerializeField] private doorLockerScript doorLockerScript;
    [SerializeField] private CombinationLockActivateScript combinationLockActivate;
    [SerializeField] private ChemicalMixingPlace chemicalMixingPlaceScript;

    [SerializeField] private float openSpeed = 5f;

    private bool isClosing = false;


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

        if (combinationLockActivate != null)
        {
            //office - washroom - safe puzzle
            combinationLockActivate.onLockPuzzleCompletion.AddListener(canBeMoved);
        }

        if (chemicalMixingPlaceScript != null)
        {
            //washroom - chemical puzzle
            chemicalMixingPlaceScript.puzzleComplete.AddListener(doorBase.UnlockDoor);
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
        }
        else if (doorBase.isOpen)
        {
            doorBase.CloseDoor(openSpeed);
        }
    }
    public void CloseDoorOnChemicalPuzzle()
    {
        doorBase.LockDoor();
        doorBase.CloseDoor(openSpeed);
    }
    private void canBeMoved()
    {
        //Debug.Log("I am called to change the state of the furniture");
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }
}