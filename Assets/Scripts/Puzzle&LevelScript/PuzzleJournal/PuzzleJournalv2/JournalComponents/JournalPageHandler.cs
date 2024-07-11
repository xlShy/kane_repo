using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JournalPageHandler : MonoBehaviour
{
    public static event Action<JournalContentPage> OnSetJournalPage;
    [Tooltip("MUST SET IN ORDER!")]
    public List<JournalContentPage> journalContent;

    public int setPage;


    private void OnEnable()
    {
        GrandfatherClock.OnGrandfathersClockComeplete += SetChangingPage;
        CombinationLockActivateScript.OnCombinationLockComplete += SetChangingPage;
    }
    private void OnDisable()
    {
        GrandfatherClock.OnGrandfathersClockComeplete -= SetChangingPage;
        CombinationLockActivateScript.OnCombinationLockComplete -= SetChangingPage;
    }
    private void SetChangingPage()
    {
        setPage++;
        SetPage();
    }
    private void SetPage()
    {
        OnSetJournalPage(journalContent[setPage]);
    }
}
