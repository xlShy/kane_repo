using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyItemInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemPanelParentObject;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private Transform itemName;
    [SerializeField] private Transform amount;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;

    public List<GameObject> itemPanels;

    private int initialItemCount = 1;

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
            //nameText = panel.transform.Find(itemName.name).GetComponent<TextMeshProUGUI>();
            if (nameText.text == keyItem.itemName)
            {
                //amountText = panel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
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

        itemPanels.Add(itemPanel);
    }

    private void UpdateItemCount(GameObject itemPanel)
    {
        amountText = itemPanel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
        int currentCount = int.Parse(amountText.text);
        currentCount++;
        SetCount(itemPanel, currentCount);
    }
    private void SetCount(GameObject itemPanel, int count)
    {
        amountText = itemPanel.transform.Find(amount.name).GetComponent<TextMeshProUGUI>();
        amountText.text = count.ToString();
    }
}
