using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingIngredientData : InventoryItem
{
    public override void Use()
    {
        Debug.Log("Placed for the sake of placing");
    }
}
