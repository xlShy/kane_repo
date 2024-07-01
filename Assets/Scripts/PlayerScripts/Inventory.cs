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

    private bool isOpen = false;
    private List<InventoryItem> consumableItemsInventory = new List<InventoryItem>();
    private List<InventoryItem> keyItemsInventory = new List<InventoryItem>();

    public GameObject itemPanelParentObject;
    public GameObject itemBoxPanel;

    private void Start()
    {
        InitializeBoxes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            consumableInventory.SetActive(isOpen);
            if (isOpen)
            {
                UpdateConsumableInventoryUI();
            }
        }
    }

    // Adds items to player inventory
    public void AddItem(InventoryItem consumableItem)
    {
        consumableItemsInventory.Add(consumableItem);
        Debug.Log(consumableItem.itemName + " added to consumables inventory.");
        if (isOpen)
        {
            UpdateConsumableInventoryUI();
        }
    }
    public void AddKeyItem(InventoryItem keyItem)
    {
        keyItemsInventory.Add(keyItem);
        Debug.Log(keyItem.itemName + "added to key items inventory");
        if (isOpen)
        {
            UpdateConsumableInventoryUI();
        }
    }
    // Counts specific item in player inventory
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
    private void UpdateKeyItemInventoryUI()
    {

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
    private void UpdateBook(Image itemIcon, string description)
    {

    }
}
