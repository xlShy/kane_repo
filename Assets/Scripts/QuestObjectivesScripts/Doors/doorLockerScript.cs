using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class doorLockerScript : MonoBehaviour
{
    public UnityEvent doorLocked;

    void Start()
    {
        StartCoroutine(InvokeDoorLockedWithDelay());
    }
    IEnumerator InvokeDoorLockedWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        doorLocked.Invoke();
        //Debug.Log("doorLockerScript: doorLocked event invoked");
    }
}