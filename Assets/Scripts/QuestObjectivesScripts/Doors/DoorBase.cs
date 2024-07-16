using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorBase : MonoBehaviour
{
    public bool canOpen = true;
    public bool isOpen = false;
    public bool isClosing = false;
    public bool isRotating = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public AudioSource doorisOpen;
    public AudioSource doorisClosed;
    public AudioSource doorIsLocked;

    [SerializeField] private GameObject doorAnchor;
    private float openSpeed = 5f;

    private void Start()
    {
        closedRotation = doorAnchor.transform.rotation;
        openRotation = Quaternion.Euler(0, 90, 0);
    }
    public void OpenDoor(float openSpeed)
    {
        if (!isRotating)
        {
            StartCoroutine(OpenDoorAnimation(openSpeed));
        }
    }
    public void CloseDoor(float closeSpeed)
    {
        if(!isRotating)
        {
            StartCoroutine(CloseDoorAnimation(closeSpeed));
        }
    }
    public void LockDoor()
    {
        //Debug.Log("DoorSystemScript: LockDoor called. Setting canOpen to false.");
        //StartCoroutine(CloseDoorAnimation(openSpeed));
        canOpen = false;
    }

    public void UnlockDoor()
    {
        //Debug.Log("DoorSystemScript: LockDoor called. Setting canOpen to false.");
        //StartCoroutine(CloseDoorAnimation(openSpeed));
        canOpen = true;
    }

    private IEnumerator OpenDoorAnimation(float openSpeed)
    {
        isRotating = true;
        while(isRotating)
        {
            print("Open Door");
            doorisOpen.Play();
            while (Quaternion.Angle(doorAnchor.transform.rotation, openRotation) > 0.01f)
            {
                doorAnchor.transform.rotation = Quaternion.Lerp(doorAnchor.transform.rotation, openRotation, Time.deltaTime * openSpeed);
                yield return null;
            }
            doorAnchor.transform.rotation = openRotation;
            isOpen = true;
            isRotating = false;
        }
    }

    private IEnumerator CloseDoorAnimation(float openSpeed)
    {
        isRotating = true;
        while(isRotating)
        {
            print("Close Door");
            if (isClosing)
            {
                //Debug.Log("DoorSystemScript: Door is already closing");
                yield break;
            }

            isClosing = true;
            //Debug.Log("DoorSystemScript: CloseDoor coroutine started");
            doorisClosed.Play();

            while (Quaternion.Angle(doorAnchor.transform.rotation, closedRotation) > 0.01f)
            {
                doorAnchor.transform.rotation = Quaternion.Lerp(doorAnchor.transform.rotation, closedRotation, Time.deltaTime * openSpeed);
                yield return null;
            }

            doorAnchor.transform.rotation = closedRotation;
            isOpen = false;
            isClosing = false;
            //Debug.Log("DoorSystemScript: Door closed");
            isRotating = false;
        }
    }
}
