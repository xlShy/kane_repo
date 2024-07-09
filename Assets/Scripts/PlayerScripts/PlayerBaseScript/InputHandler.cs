using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private bool isConsumablesOpen;
    private bool isKeyOpen;
    //EVENTS
    public static event Action<bool> OnConsumableInventoryOpen;
    public static event Action<bool> OnKeyItemInventoryOpen;
    private void Update()
    {
        KeyItemInventoryInput();
        ConsumbaleInventoryInput();
    }
    public void ConsumbaleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isConsumablesOpen = !isConsumablesOpen;

            //Invokes in the Inventory, a method SetConsumableOpen is subscribed.
            OnConsumableInventoryOpen?.Invoke(isConsumablesOpen);
        }
    }
    public void KeyItemInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isKeyOpen = !isKeyOpen;

            //Invokes in the Inventory, a method SetKeyItemOpen is subscribed.
            OnKeyItemInventoryOpen?.Invoke(isKeyOpen);
        }
    }
    public void OnOpenPuzzleJournal()
    {

    }
    public void ClosePuzzlesCanvas()
    {

    }
}
