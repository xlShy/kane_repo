using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    KeyItem,
    Consumables
}
public abstract class InventoryItem : MonoBehaviour
{
    [Tooltip("Choose whether the item is consumable or key items")]
    public ItemType type;

    [Tooltip("Enter the item's name")]
    public string itemName;

    [Tooltip("Insert the item's icon")]
    public Sprite inventoryItemImage;

    [Tooltip("Enter the item's description here if applicable")]
    [TextArea (3,10)]
    public string itemDescription;
}


