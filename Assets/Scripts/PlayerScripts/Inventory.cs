using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> items = new List<InventoryItem>();

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
}
