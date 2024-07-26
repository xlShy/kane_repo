using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHandler : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
