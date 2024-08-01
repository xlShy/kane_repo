using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class DoorBase : MonoBehaviour
{
    public bool canOpen = true;
    public bool isOpen = false;
    public bool isRotating = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public AudioSource doorisOpen;
    public AudioSource doorisClosed;
    public AudioSource doorIsLocked;

    [SerializeField] private GameObject doorAnchor;
    private float openSpeed = 5f;

    public UnityEvent OnDoorOpenEvent;

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
            if(OnDoorOpenEvent != null)
            {
                OnDoorOpenEvent?.Invoke();
            }
        }
    }
    public void CloseDoor(float closeSpeed)
    {
        if (!isRotating)
        {
            StartCoroutine(CloseDoorAnimation(closeSpeed));
        }
    }
    public void LockDoor()
    {
        StartCoroutine(CloseDoorAnimation(openSpeed));
        canOpen = false;
    }

    public void UnlockDoor()
    {
        canOpen = true;
    }
    private IEnumerator OpenDoorAnimation(float openSpeed)
    {
        if (isRotating)
        {
            yield break;
        }
        isRotating = true;
        doorisOpen.Play();

        var lerpValue = 0f;
        var startRotation = doorAnchor.transform.rotation;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * openSpeed;
            doorAnchor.transform.rotation = Quaternion.Lerp(startRotation, openRotation, lerpValue);
            yield return null;
        }
        doorAnchor.transform.rotation = openRotation;
        isOpen = true;
        isRotating = false;
    }

    private IEnumerator CloseDoorAnimation(float openSpeed)
    {
        if (isRotating)
        {
            yield break;
        }
        isRotating = true;
        doorisClosed.Play();

        var lerpValue = 0f;
        var startRotation = doorAnchor.transform.rotation;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * openSpeed;
            doorAnchor.transform.rotation = Quaternion.Lerp(startRotation, closedRotation, lerpValue);
            yield return null;
        }

        doorAnchor.transform.rotation = closedRotation;
        isOpen = false;
        isRotating = false;

    }
}
