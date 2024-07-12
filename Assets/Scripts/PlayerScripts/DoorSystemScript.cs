using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorSystemScript : InteractableObject
{
    private bool canOpen = true;
    private bool isOpen = false;

    [SerializeField] private ChemicalMixingEventTrigger chemMixTriggerScript;
    [SerializeField] private doorLockerScript doorLockerScript;
    [SerializeField] private CombinationLockActivateScript combinationLockActivate;

    [SerializeField] private float openSpeed = 5f;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource doorLocked;

    private void Awake()
    {
        Debug.Log("DoorSystemScript: Awake called");
    }

    private void Start()
    {
        Debug.Log("DoorSystemScript: Start called");
        if (doorLockerScript != null)
        {
            doorLockerScript.doorLocked.AddListener(LockDoor);
            Debug.Log("DoorSystemScript: Subscribed to doorLocked event");
        }
        else
        {
            Debug.LogWarning("DoorSystemScript: doorLockerScript is not assigned!");
        }

        if (chemMixTriggerScript != null)
        {
            chemMixTriggerScript.chemicalMixingEventInitiate.AddListener(LockDoor);
            Debug.Log("DoorSystemScript: Subscribed to chemicalMixingEventInitiate event");
        }

        if (combinationLockActivate != null)
        {
            combinationLockActivate.onLockPuzzleCompletion.AddListener(canBeMoved);
        }

        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, 90, 0);
    }

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        Debug.Log($"DoorSystemScript: Interact called. canOpen: {canOpen}, isOpen: {isOpen}");
        if (!isOpen && canOpen)
        {
            Debug.Log("DoorSystemScript: Opening door");
            StartCoroutine(OpenDoor());
        }
        else if (!isOpen && !canOpen)
        {
            Debug.Log("DoorSystemScript: Door is locked, playing locked sound");
            doorLocked.Play();
        }
        else if (isOpen)
        {
            Debug.Log("DoorSystemScript: Closing door");
            StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        Debug.Log("DoorSystemScript: OpenDoor coroutine started");
        doorOpen.Play();
        while (Quaternion.Angle(transform.rotation, openRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = openRotation;
        isOpen = true;
        Debug.Log("DoorSystemScript: Door opened");
    }

    private IEnumerator CloseDoor()
    {
        Debug.Log("DoorSystemScript: CloseDoor coroutine started");
        doorClose.Play();
        while (Quaternion.Angle(transform.rotation, closedRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = closedRotation;
        isOpen = false;
        Debug.Log("DoorSystemScript: Door closed");
    }

    private void LockDoor()
    {
        Debug.Log("DoorSystemScript: LockDoor called. Setting canOpen to false.");
        canOpen = false;
    }

    private void canBeMoved()
    {
        Debug.Log("I am called to change the state of the furniture");
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }
}