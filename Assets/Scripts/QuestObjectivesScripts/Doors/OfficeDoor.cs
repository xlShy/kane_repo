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
        ChemicalMixingPlace.OnCompleteOfficePuzzle += doorBase.UnlockDoor;
    }
    private void OnDisable()
    {
        ChemicalMixingPlace.OnCompleteOfficePuzzle -= doorBase.UnlockDoor;
    }

}
