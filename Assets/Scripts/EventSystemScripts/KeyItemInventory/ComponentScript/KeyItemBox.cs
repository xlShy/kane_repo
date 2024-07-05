using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemBox : MonoBehaviour
{
    public InventoryItem keyItem;
    public KeyItemInventory controller;

    public void OnClickItemBox()
    {
        controller.ShowKeyItemData(keyItem);
    }
    public void SetKeyItemData(InventoryItem keyItemData, KeyItemInventory UIController)
    {
        keyItem = keyItemData;
        controller = UIController;
    }
}
