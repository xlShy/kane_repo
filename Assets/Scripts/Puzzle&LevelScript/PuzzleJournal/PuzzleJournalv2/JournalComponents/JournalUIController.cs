using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour
{
    [SerializeField] private GameObject currentPageDisplayed;

    [SerializeField] private List<Sprite> startingPages;
    [SerializeField] private List<Sprite> journalPages;

    [SerializeField] private int currentPage = 0;

    bool isOpen;
    private void OnEnable()
    {
        JournalPageHandler.OnSetJournalPage += ChangeJournalPage;
    }
    private void OnDisable()
    {
        JournalPageHandler.OnSetJournalPage -= ChangeJournalPage;
    }
    private void Start()
    {
        SetStartingJournalPage();
        currentPageDisplayed.GetComponent<Image>().sprite = journalPages[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextPage();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousPage();
        }
    }
    private void SetStartingJournalPage()
    {
       journalPages.AddRange(startingPages);
    }
    private void ChangeJournalPage(JournalContentPage journalPage)
    {
        int pageIndex = journalPage.pageNumber - 1; 

        if (pageIndex >= 0 && pageIndex < journalPages.Count)
        {
            if (journalPage.pages.Count > 0)
            {
                journalPages[pageIndex] = journalPage.pages[0];

                if (journalPage.pages.Count > 1)
                {
                    if (pageIndex + 1 < journalPages.Count)
                    {
                        journalPages[pageIndex + 1] = journalPage.pages[1];
                    }
                    else
                    {
                        journalPages.Add(journalPage.pages[1]);
                    }
                }
                if (currentPage == pageIndex || currentPage == pageIndex + 1)
                {
                    UpdateCurrentPageDisplay();
                }
            }
        }
    }
    private void NextPage()
    {
        if (currentPage < journalPages.Count - 1)
        {
            currentPage++;
            UpdateCurrentPageDisplay();
        }
    }
    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateCurrentPageDisplay();
        }
    }
    private void UpdateCurrentPageDisplay()
    {
        if (currentPageDisplayed != null && journalPages.Count > 0)
        {
            currentPageDisplayed.GetComponent<Image>().sprite = journalPages[currentPage];
        }
    }
}
