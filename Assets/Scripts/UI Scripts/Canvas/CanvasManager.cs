using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public List<GameObject> UICanvas;
    public List<GameObject> PuzzleCanvas;
    public GameObject enabledCanvas;

    private void Update()
    {
        CheckEnabledCanvas();
    }
    private void DisableAllCanvas()
    {
        foreach(GameObject canvas in UICanvas)
        {
            canvas.SetActive(false);
        }
    }
    private void CheckEnabledCanvas()
    {
        foreach(GameObject canvas in UICanvas)
        {
            if (canvas == canvas.activeSelf)
            {
                enabledCanvas = canvas;
                SetCursorEnabled();
            }
        }
        if (enabledCanvas != null)
        {
            foreach (GameObject canvas in UICanvas)
            {
                if (canvas != enabledCanvas)
                {
                    canvas.SetActive(false);
                }
            }
        } 
        else if(enabledCanvas == null)
        {
            SetCursorDisabled();
        }
    }
    private void SetCursorDisabled()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void SetCursorEnabled()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
