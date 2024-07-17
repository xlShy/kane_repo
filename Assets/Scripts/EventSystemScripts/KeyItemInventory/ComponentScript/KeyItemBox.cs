using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class KeyItemBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem keyItem;
    public KeyItemInventory controller;

    public Image buttonImage;
    public float highlightedAlpha = 1f;
    public float normalAlpha = 0f;

    public TextMeshProUGUI itemNameText; 
    public TextMeshProUGUI itemAmount;
    public Color highlightedTextColor = Color.white;
    public Color normalTextColor = Color.black; 
    private void Start()
    {
        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }
        if (itemNameText == null)
        {
            itemNameText = GetComponentInChildren<TextMeshProUGUI>();
        }
        SetVisualState(normalAlpha, normalTextColor);
    }

    public void OnClickItemBox()
    {
        controller.ShowKeyItemData(keyItem);
    }
    public void SetKeyItemData(InventoryItem keyItemData, KeyItemInventory UIController)
    {
        keyItem = keyItemData;
        controller = UIController;
        if (itemNameText != null)
        {
            itemNameText.text = keyItem.itemName;    
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetVisualState(highlightedAlpha, highlightedTextColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetVisualState(normalAlpha, normalTextColor);
    }

    private void SetVisualState(float alpha, Color textColor)
    {
        if (buttonImage != null)
        {
            Color newColor = buttonImage.color;
            newColor.a = alpha;
            buttonImage.color = newColor;
        }

        if (itemNameText != null)
        {
            itemNameText.color = textColor;
            itemAmount.color = textColor;
        }
    }
}
