using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumableItemInventoryScript : MonoBehaviour
{
    [SerializeField] private Inventory inventoryScript;
    [SerializeField] private Image[] itemSlots;
    private bool isCursorVisible = true;

    private void Start()
    {
        inventoryScript.consumableInventoryOpen.AddListener(consumableOpen);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i;
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => OnItemClick(index));
        }
    }

    private void consumableOpen()
    {
        ToggleCursorState();
    }

    private void ToggleCursorState()
    {
        isCursorVisible = !isCursorVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnItemClick(int slotIndex)
    {
        InventoryItem item = inventoryScript.GetConsumableItemAtIndex(slotIndex);
        if (item != null)
        {
            UseItem(slotIndex);
        }
    }

    private void UseItem(int slotIndex)
    {
        InventoryItem item = inventoryScript.GetConsumableItemAtIndex(slotIndex);
        if (item != null)
        {
            item.Use();
            inventoryScript.RemoveConsumableItemAtIndex(slotIndex);
            UpdateUISlot(slotIndex);
        }
    }

    private void UpdateUISlot(int slotIndex)
    {
        InventoryItem item = inventoryScript.GetConsumableItemAtIndex(slotIndex);
        if (item != null)
        {
            itemSlots[slotIndex].sprite = item.inventoryItemImage;
            itemSlots[slotIndex].color = Color.white;
        }
        else
        {
            itemSlots[slotIndex].sprite = null;
            itemSlots[slotIndex].color = Color.clear;
        }
    }
}
