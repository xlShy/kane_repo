using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventDdata)
    {
        HoverPanel.SetActive(false);
    }
}

