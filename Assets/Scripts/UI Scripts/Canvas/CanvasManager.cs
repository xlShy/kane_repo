using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasManager : CanvasToggler
{
    private List<GameObject> UICanvas;
    private List<GameObject> PuzzleCanvas;
    public GameObject enabledCanvas;

    public static event Action<bool> OnCanvasEnabled;
    private void Start()
    {
        UICanvas = new List<GameObject>();
        UICanvas.Add(consumableInventory);
        UICanvas.Add(keyItemInventory);
        UICanvas.Add(journal);
    }
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
            if (canvas.activeSelf)
            {
                enabledCanvas = canvas;
                SetCursorEnabled();
                OnCanvasEnabled?.Invoke(true);
                return;
            }
            else if(!canvas.activeSelf)
            {
                enabledCanvas = null;
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
            OnCanvasEnabled?.Invoke(false);
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
