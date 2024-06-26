using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject consumableInventory;
    [SerializeField]
    private GameObject box1;
    [SerializeField]
    private GameObject box2;
    [SerializeField]
    private GameObject box3;

    private bool isOpen;
    private List<InventoryItem> items = new List<InventoryItem>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {
            consumableInventory.SetActive(true);
            UpdateInventoryUI();
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            consumableInventory.SetActive(false);
            isOpen = false;
        }

    }
    //adds items to player inventory
    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " added to inventory.");
    }

    // counts specific item in player inventory
    public int GetItemCount<T>() where T : InventoryItem
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item is T)
            {
                count++;
            }
        }
        return count;
    }

    // determines if player has specific item in inventory
    public bool HasItem<T>() where T : InventoryItem
    {
        return GetItemCount<T>() > 0;
    }

    public bool RemoveItem<T>(T item) where T : InventoryItem
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            return true;
        }
        return false;
    }

    public List<T> GetAllItemsOfType<T>() where T : InventoryItem
    {
        List<T> result = new List<T>();
        foreach (InventoryItem item in items)
        {
            if (item is T)
            {
                result.Add(item as T);
            }
        }
        return result;
    }

    private void UpdateInventoryUI()
    {
        UpdateBox(box1, items.Count > 0 ? items[0] : null);
        UpdateBox(box2, items.Count > 1 ? items[1] : null);
        UpdateBox(box3, items.Count > 2 ? items[2] : null);
    }

    private void UpdateBox(GameObject box, InventoryItem item)
    {
        Image itemImage = box.GetComponentInChildren<Image>();

        if (item != null)
        {
            itemImage.sprite = item.inventoryItemImage;
            itemImage.enabled = true;

        }
        else
        {
            itemImage.enabled = false;
        }
    }
}
