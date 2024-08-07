using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoorScript : InteractableObject
{
    private DoorBase doorBase;
    private Level1Completed level1Completed;

    [SerializeField] private float openSpeed = 5f;
    [SerializeField] private doorLockerScript doorLockerScript;
    private void OnEnable()
    {
        //LevelManager.OnCompleteLevel1 += UnlockDoor;
    }
    private void Awake()
    {
        doorBase = GetComponent<DoorBase>();
        level1Completed = GetComponent<Level1Completed>();
    }
    private void Start()
    {
        doorBase.canOpen = false;
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (!doorBase.isOpen && doorBase.canOpen)
        {
            doorBase.OpenDoor(openSpeed);
            level1Completed.CompleteLevel1();
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
    private void UnlockDoor()
    {
        print("basement is open");
        doorBase.UnlockDoor();
    }
}
