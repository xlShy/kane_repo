using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime;

public class Inventory : MonoBehaviour
{
    private bool isKeyOpen = false;
    [SerializeField] private GameObject keyItemInventory;

    private bool isConsumablesOpen = false;
    [SerializeField] private GameObject consumableInventory;

    [Header("UI on Consumable Inventory")]
    [SerializeField]
    private GameObject box1;
    [SerializeField]
    private GameObject box2;
    [SerializeField]
    private GameObject box3;

    //Player Inventories
    private List<InventoryItem> consumableItemsInventory = new List<InventoryItem>();
    public List<InventoryItem> keyItemsInventory = new List<InventoryItem>(); //TO DO - Set to private later

    [SerializeField] private KeyItemInventory keyItemInventoryScript;

    public static event Action<bool> onInventoryFull;

    public bool isFull;

    [SerializeField] private List<GameObject> gameCanvases;


    private void Start()
    {
        InitializeBoxes();
    }

    // Adds items to player inventory
    public void AddItem(InventoryItem item)
    {
        if (item.type == ItemType.Consumables)
        {
            if (consumableItemsInventory.Count >= 3)
            {
                isFull = true;
                onInventoryFull?.Invoke(isFull);
                return;
            }
            consumableItemsInventory.Add(item);
        }
        else if (item.type == ItemType.KeyItem)
        {
            keyItemsInventory.Add(item);
            keyItemInventoryScript.CreateOrUpdateItemPanelBox(item, keyItemsInventory);
        }

        isFull = false;
        onInventoryFull?.Invoke(isFull);

        if (isConsumablesOpen)
        {
            UpdateConsumableInventoryUI();
        }
        UpdateConsumableInventoryUI();
    }

    public InventoryItem GetConsumableItemAtIndex(int index)
    {
        if (index >= 0 && index < consumableItemsInventory.Count)
        {
            return consumableItemsInventory[index];
        }
        return null;
    }

    public void RemoveConsumableItemAtIndex(int index)
    {
        if (index >= 0 && index < consumableItemsInventory.Count)
        {
            consumableItemsInventory.RemoveAt(index);
            UpdateConsumableInventoryUI();
        }
        UpdateConsumableInventoryUI();
    }
    public void AddKeyItem(InventoryItem keyItem)
    {
        keyItemsInventory.Add(keyItem);

        keyItemInventoryScript.CreateOrUpdateItemPanelBox(keyItem, keyItemsInventory);
    }

    //TO DO - put function in a separate script

    public List<InventoryItem> GetKeyItems()
    {
        return keyItemsInventory;
    }

    public int GetItemCount<T>() where T : InventoryItem
    {
        int count = 0;
        foreach (var item in consumableItemsInventory)
        {
            if (item is T)
            {
                count++;
            }
        }
        return count;
    }

    // Determines if player has specific item in inventory

    public bool HasItem<T>() where T : InventoryItem
    {
        return GetItemCount<T>() > 0;
    }
    public bool RemoveItem<T>(T item) where T : InventoryItem
    {
        if (consumableItemsInventory.Contains(item))
        {
            consumableItemsInventory.Remove(item);
            return true;
        }
        return false;
    }
    //TO DO - put function in another script
    public List<T> GetAllItemsOfType<T>() where T : InventoryItem
    {
        List<T> result = new List<T>();
        foreach (InventoryItem item in consumableItemsInventory)
        {
            if (item is T)
            {
                result.Add(item as T);
            }
        }
        return result;
    }

    // Ensures all parent images are active and initializes child images
    private void InitializeBoxes()
    {
        InitializeBox(box1);
        InitializeBox(box2);
        InitializeBox(box3);
    }

    private void InitializeBox(GameObject box)
    {
        box.SetActive(true); // Ensure parent boxes are always active
        Image[] images = box.GetComponentsInChildren<Image>(true);
        if (images.Length > 1)
        {
            Image itemImage = images[1];
            itemImage.enabled = false;   // Initialize the child image as disabled
            //Debug.Log($"Initialized {box.name} child image as disabled.");
        }
    }

    private void UpdateConsumableInventoryUI()
    {
        UpdateBox(box1, consumableItemsInventory.Count > 0 ? consumableItemsInventory[0] : null);
        UpdateBox(box2, consumableItemsInventory.Count > 1 ? consumableItemsInventory[1] : null);
        UpdateBox(box3, consumableItemsInventory.Count > 2 ? consumableItemsInventory[2] : null);
    }
    

    private void UpdateBox(GameObject box, InventoryItem item)
    {
        Image[] images = box.GetComponentsInChildren<Image>(true);
        Debug.Log(item);

        if (images.Length > 1)
        {
            //Debug.Log("I've entered");
            Image itemImage = images[1];
            if (item != null)
            {
                //Debug.Log($"{box.name} - Item found, setting image.");
                itemImage.sprite = item.inventoryItemImage;
                itemImage.enabled = true;
            }
            else
            {
                //Debug.Log($"{box.name} - No item found, disabling image.");
                itemImage.enabled = false;
            }
        }
    }
    
}
