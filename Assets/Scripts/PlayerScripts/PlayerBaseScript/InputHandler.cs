using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool isUIOpen;

    private KeyCode currentOpenedUIKey;

    //EVENTS
    public static event Action<bool> OnConsumableInventoryOpen;
    public static event Action<bool> OnKeyItemInventoryOpen;
    public static event Action<bool> OnJournalOpen;

    private void OnEnable()
    {
        CanvasManager.OnCanvasEnabled += SetIsOnPuzzle;
    }
    private void OnDisable()
    {
        CanvasManager.OnCanvasEnabled -= SetIsOnPuzzle;
    }
    private void Update()
    {
        if (!isUIOpen)
        {
            CheckOpenUI();
        }
        else
        {
            CheckCloseUI();
        }
    }

    private void CheckOpenUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenUI(KeyCode.Tab, OnConsumableInventoryOpen);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            OpenUI(KeyCode.B, OnKeyItemInventoryOpen);
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            OpenUI(KeyCode.J, OnJournalOpen);
        }
    }

    private void CheckCloseUI()
    {
        if (Input.GetKeyDown(currentOpenedUIKey))
        {
            CloseUI();
        }
    }

    private void OpenUI(KeyCode key, Action<bool> openEvent)
    {
        isUIOpen = true;
        currentOpenedUIKey = key;
        openEvent?.Invoke(true);
    }

    private void CloseUI()
    {
        isUIOpen = false;
        
        if (currentOpenedUIKey == KeyCode.Tab)
        {
            OnConsumableInventoryOpen?.Invoke(false);
        }
        else if (currentOpenedUIKey == KeyCode.B)
        {
            OnKeyItemInventoryOpen?.Invoke(false);
        }
        else if (currentOpenedUIKey == KeyCode.J)
        {
            OnJournalOpen?.Invoke(false);
        }
        currentOpenedUIKey = KeyCode.None;
    }
    private void SetIsOnPuzzle(bool isCanvasOn)
    {
        isUIOpen = isCanvasOn;
    }
    public void ClosePuzzlesCanvas()
    {
        // Implementation for closing puzzle canvas
    }
}