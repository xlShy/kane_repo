using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDoor : MonoBehaviour
{
    private DoorBase doorBase;

    private void Awake()
    {
        doorBase = GetComponent<DoorBase>();
    }
    private void OnEnable()
    {
        ReadableDocumentScript.OnStartOfficePuzzle += doorBase.LockDoor;
        ChemicalMixingPlace.OnCompleteOfficePuzzle += doorBase.UnlockDoor;
    }
    private void OnDisable()
    {
        ReadableDocumentScript.OnStartOfficePuzzle -= doorBase.LockDoor;
        ChemicalMixingPlace.OnCompleteOfficePuzzle -= doorBase.UnlockDoor;
    }

}
