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
        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i;
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => OnItemClick(index));
        }
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
            itemSlots[slotIndex].enabled = true;
        }
        else
        {
            itemSlots[slotIndex].sprite = null;
            itemSlots[slotIndex].enabled = false;
        }
    }
}
