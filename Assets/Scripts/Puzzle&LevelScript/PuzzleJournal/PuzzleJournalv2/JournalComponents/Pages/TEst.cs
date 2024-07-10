using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TEst : MonoBehaviour
{
    public static event Action<JournalContentPage> OnCompleteEvent;
    public JournalContentPage page1;
    public JournalContentPage page2;
    public JournalContentPage page3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print("Book Updated");
            OnCompleteEvent?.Invoke(page1);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            print("Book Updated");
            OnCompleteEvent?.Invoke(page2);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            print("Book Updated");
            OnCompleteEvent?.Invoke(page3);
        }
    }
}
