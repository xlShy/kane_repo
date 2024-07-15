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

    public int setPage = 0;


    private void OnEnable()
    {
        GrandfatherClock.OnGrandfathersClockComplete += SetChangingPage;
        CombinationLockActivateScript.OnCombinationLockComplete += SetChangingPage;
    }
    private void OnDisable()
    {
        GrandfatherClock.OnGrandfathersClockComplete -= SetChangingPage;
        CombinationLockActivateScript.OnCombinationLockComplete -= SetChangingPage;
    }
    private void SetChangingPage()
    {
        SetPage();
        setPage++;
    }
    private void SetPage()
    {
        OnSetJournalPage(journalContent[setPage]);
    }
}
