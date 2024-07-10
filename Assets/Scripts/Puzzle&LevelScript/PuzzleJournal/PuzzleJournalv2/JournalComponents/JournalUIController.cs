using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour
{
    [SerializeField] private GameObject journalCanvas;
    [SerializeField] private GameObject currentPageDisplayed;

    [SerializeField] private List<Sprite> startingPages;
    [SerializeField] private List<Sprite> journalPages;

    [SerializeField] private int currentPage = 0;

    bool isOpen;
    private void OnEnable()
    {
        TEst.OnCompleteEvent += ChangeJournalPage;
    }
    private void OnDisable()
    {
        TEst.OnCompleteEvent -= ChangeJournalPage;
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
        //set the default page at the start of the game
       journalPages.AddRange(startingPages);
    }
    private void ChangeJournalPage(JournalContentPage journalPage)
    {
        int pageIndex = journalPage.pageNumber - 1;  // Subtract 1 here

        if (pageIndex >= 0 && pageIndex < journalPages.Count)
        {
            // Check if there are pages to add
            if (journalPage.pages.Count > 0)
            {
                // Replace the current page
                journalPages[pageIndex] = journalPage.pages[0];

                // If there's a second page, add or replace the next page
                if (journalPage.pages.Count > 1)
                {
                    if (pageIndex + 1 < journalPages.Count)
                    {
                        // Replace the next page
                        journalPages[pageIndex + 1] = journalPage.pages[1];
                    }
                    else
                    {
                        // Add a new page
                        journalPages.Add(journalPage.pages[1]);
                    }
                }

                // Update the display if we're on one of the changed pages
                if (currentPage == pageIndex || currentPage == pageIndex + 1)
                {
                    UpdateCurrentPageDisplay();
                }
            }
        }
    }
    private void NextPage()
    {
        //set currentPage to the next page
        if (currentPage < journalPages.Count - 1)
        {
            currentPage++;
            UpdateCurrentPageDisplay();
        }
    }
    private void PreviousPage()
    {
        //set currentPage to the previous page
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
