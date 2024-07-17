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
    public GameObject currentEnabledCanvas;

    public bool isCanvasFound;

    public static event Action<bool> OnCanvasEnabled;


    private void Start()
    {
        UICanvas = new List<GameObject>();
        UICanvas.Add(consumableInventory);
        UICanvas.Add(keyItemInventory);
        UICanvas.Add(journal);

        PuzzleCanvas = new List<GameObject>();
        PuzzleCanvas.Add(grandFathersClock);
        PuzzleCanvas.Add(combinationLock);
        PuzzleCanvas.Add(tvInterface);

        DisableAllCanvas(UICanvas);
        DisableAllCanvas(PuzzleCanvas);
    }
    private void Update()
    {
        isCanvasFound = false;

        CheckEnabledCanvas(PuzzleCanvas);
        if (!isCanvasFound)
        {
            CheckEnabledCanvas(UICanvas);
        }
    }
    private void DisableAllCanvas(List<GameObject> canvasList)
    {
        foreach(GameObject canvas in canvasList)
        {
            canvas.SetActive(false);
        }
    }
    private void CheckEnabledCanvas(List<GameObject> canvasList)
    {
        currentEnabledCanvas = null;
        foreach (GameObject canvas in canvasList)
        {
            if(canvas == null)
            {
                return;
            }
            if (canvas.activeSelf)
            {
                currentEnabledCanvas = canvas;
                isCanvasFound = true;
                SetCursorEnabled();
                OnCanvasEnabled?.Invoke(true);
                return;
            }
        }
        if (currentEnabledCanvas != null)
        {
            foreach (GameObject canvas in canvasList)
            {
                if (canvas != currentEnabledCanvas)
                {
                    canvas.SetActive(false);
                }
            }
        } 
        else if(currentEnabledCanvas == null)
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
