using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoorScript : MonoBehaviour
{
    private BathroomDoor doorSystemScript;
    private void OnEnable()
    {
        LevelManager.OnCompleteLevel1 += UnlockDoor;
    }
    private void Awake()
    {
        doorSystemScript = GetComponent<BathroomDoor>();
    }
    private void Start()
    {
        //doorSystemScript.LockDoor();
    }
    private void UnlockDoor()
    {
        //doorSystemScript.UnlockDoor();
    }
}
