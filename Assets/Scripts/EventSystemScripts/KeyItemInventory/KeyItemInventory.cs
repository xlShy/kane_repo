using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemInventory : MonoBehaviour
{
    [Header("UI for ItemBoxes")]
    [SerializeField] private GameObject itemPanelParentObject;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private Transform itemName;
    [SerializeField] private Transform amount;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;

    public List<GameObject> itemPanels;
    private int initialItemCount = 1;

    [Header("UI on Clicking Item Boxes")]
    [SerializeField] private GameObject keyItemIconHolder;
    [SerializeField] private GameObject keyItemDescription;

    public void CreateOrUpdateItemPanelBox(InventoryItem keyItem, List<InventoryItem> keyItemInventory)
    {
        bool itemFound = false;

        foreach (GameObject panel in itemPanels)
        {
            nameText = panel.transform.Find(itemName.name).GetComponent<TextMeshProUGUI>();
            if (nameText.text == keyItem.itemName)
            {
                UpdateItemCount(panel);
                itemFound = true;
                return;
            }
        }
        if (!itemFound)
        {
            InstantiateItemBoxPanel(keyItem);
        }
    }
    public void RemoveKeyItem(InventoryItem keyItem, int itemCount)
    {
        foreach (GameObject panel in itemPanels)
        {
            if (nameText.text == keyItem.itemName)
            {
                int currentCount = int.Parse(amountText.text);

                if (currentCount > itemCount)
                {
                    SetCount(panel, currentCount - itemCount);
                }
                else
                {
                    itemPanels.Remove(panel);
                    Destroy(panel);
                }
                return;
            }
        }
    }
    private void InstantiateItemBoxPanel(InventoryItem keyItem)
    {
        GameObject itemPanel = Instantiate(itemBox, itemPanelParentObject.transform);

        nameText = itemPanel.transform.Find(itemName.name).GetComponent<TextMeshProUGUI>();
        nameText.text = keyItem.itemName;

        SetCount(itemPanel, initialItemCount);

        KeyItemBox keyItemBox = itemPanel.GetComponent<KeyItemBox>();
        if (keyItemBox != null)
        {
            keyItemBox.SetKeyItemData(keyItem, this);
        }

        itemPanels.Add(itemPanel);
    }
    private void UpdateItemCount(GameObject itemPanel)
    {
        int currentCount = int.Parse(amountText.text);
        currentCount++;
        SetCount(itemPanel, currentCount);
    }
    private void SetCount(GameObject itemPanel, int count) //Set item count if item is stackable
    {
        amountText = itemPanel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
        amountText.text = count.ToString();
    }
    public void ShowKeyItemData(InventoryItem keyItem) //Called to show key item data in inventory on click
    {
        keyItemIconHolder.GetComponent<Image>().sprite = keyItem.inventoryItemImage;
        keyItemDescription.GetComponent<TextMeshProUGUI>().text = keyItem.itemDescription;
    }

}
