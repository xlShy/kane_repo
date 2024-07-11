using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JournalPageHandler : MonoBehaviour
{
    public static event Action<JournalContentPage> OnSetJournalPage;
    public JournalContentPage page1;
    public JournalContentPage page2;
    public JournalContentPage page3;

    private void OnEnable()
    {
        GrandfatherClock.OnGrandfathersClockComeplete += SetPage1;
        CombinationLockActivateScript.OnCombinationLockComplete += SetPage2;
    }
    private void OnDisable()
    {
        GrandfatherClock.OnGrandfathersClockComeplete -= SetPage1;
        CombinationLockActivateScript.OnCombinationLockComplete -= SetPage2;
    }
    private void SetPage1()
    {
        OnSetJournalPage(page1);
    }
    private void SetPage2()
    {
        OnSetJournalPage(page2);
    }

}
