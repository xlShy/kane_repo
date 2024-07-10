using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private bool isUIOpen;
    private KeyCode currentOpenedUIKey;

    //EVENTS
    public static event Action<bool> OnConsumableInventoryOpen;
    public static event Action<bool> OnKeyItemInventoryOpen;
    public static event Action<bool> OnPuzzleJournalOpen;
    public static event Action<bool> OnJournalOpen;

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
        else if (Input.GetKeyDown(KeyCode.J))
        {
            OpenUI(KeyCode.J, OnPuzzleJournalOpen);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            OpenUI(KeyCode.K, OnJournalOpen);
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
        else if(currentOpenedUIKey == KeyCode.J)
        {
            OnPuzzleJournalOpen?.Invoke(false);
        }
        else if (currentOpenedUIKey == KeyCode.K)
        {
            OnJournalOpen?.Invoke(false);
        }
        currentOpenedUIKey = KeyCode.None;
    }

    public void ClosePuzzlesCanvas()
    {
        // Implementation for closing puzzle canvas
    }
}