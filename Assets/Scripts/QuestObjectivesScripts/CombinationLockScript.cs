using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CombinationLockScript : MonoBehaviour
{
    public Text[] digitTexts;
    private int[] digits;
    private int[] correctCombination = { 1, 2, 3, 4 };
    private Renderer objectRenderer;

    public UnityEvent onCorrectCombinationEntered;

    public void Start()
    {
        digits = new int[digitTexts.Length];
        UpdateDigitTexts();
    }

    public void UpdateDigitTexts()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            digitTexts[i].text = digits[i].ToString();
        }
        CheckCombination();
    }

    public void OnScroll(PointerEventData eventData, int digitIndex)
    {
        if (eventData.scrollDelta.y > 0)
        {
            digits[digitIndex] = (digits[digitIndex] + 1) % 10;
        }
        else if (eventData.scrollDelta.y < 0)
        {
            digits[digitIndex] = (digits[digitIndex] - 1 + 10) % 10;
        }
        UpdateDigitTexts();
    }

    public void OnScrollDigit0(BaseEventData eventData)
    {
        OnScroll((PointerEventData)eventData, 0);
    }

    public void OnScrollDigit1(BaseEventData eventData)
    {
        OnScroll((PointerEventData)eventData, 1);
    }

    public void OnScrollDigit2(BaseEventData eventData)
    {
        OnScroll((PointerEventData)eventData, 2);
    }

    public void OnScrollDigit3(BaseEventData eventData)
    {
        OnScroll((PointerEventData)eventData, 3);
    }

    private void CheckCombination()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i] != correctCombination[i])
            {
                return;
            }
        }

        onCorrectCombinationEntered.Invoke();
    }
}
