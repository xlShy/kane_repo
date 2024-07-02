using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyItemInventory : MonoBehaviour
{
    public GameObject itemPanelParentObject;
    public GameObject itemBox;
    public Transform itemName;
    public Transform amount;
    public List<GameObject> itemPanels;

     int count = 1;
    public void CreateOrUpdateItemPanelBox(InventoryItem keyItem, List<InventoryItem> keyItemInventory)
    {
        bool itemFound = false;

        foreach (GameObject panel in itemPanels)
        {
            TextMeshProUGUI nameText = panel.transform.Find(itemName.name).GetComponent<TextMeshProUGUI>();
            if (nameText.text == keyItem.itemName)
            {
                UpdateItemCount(panel);
                itemFound = true;
                Debug.Log("Item is duplicate");
                return;
            }
        }
        if (!itemFound)
        {
            InstantiateItemBoxPanel(keyItem);
            Debug.Log("New item found");
        }
    }

    public void InstantiateItemBoxPanel(InventoryItem keyItem)
    {
        GameObject itemPanel = Instantiate(itemBox, itemPanelParentObject.transform);
        Debug.Log(itemPanel);

        TextMeshProUGUI nameText = itemPanel.transform.Find(itemName.name).GetComponent<TextMeshProUGUI>();
        nameText.text = keyItem.itemName;

        SetCount(itemPanel, 1);

        itemPanels.Add(itemPanel);
    }

    private void UpdateItemCount(GameObject itemPanel)
    {
        TextMeshProUGUI amountText = itemPanel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
        int currentCount = int.Parse(amountText.text);
        currentCount++;
        SetCount(itemPanel, currentCount);
    }
    private void SetCount(GameObject itemPanel, int count)
    {
        TextMeshProUGUI amountText = itemPanel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
        amountText.text = count.ToString();
    }
}
