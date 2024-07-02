using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDoorScript : MonoBehaviour
{
    [SerializeField]
    public GrandfatherClock grandFatherClockScript;

    private void Start()
    {
        grandFatherClockScript.doorOpen.AddListener(OpenOfficeDoor);
    }

    private void OpenOfficeDoor()
    {
        gameObject.SetActive(false);
    }
}
