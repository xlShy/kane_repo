using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggler : MonoBehaviour
{
    [SerializeField] protected GameObject keyItemInventory;
    [SerializeField] protected GameObject consumableInventory;

    [SerializeField] protected GameObject puzzleJournal;
    [SerializeField] protected GameObject journal;

    public void OnEnable()
    {
        InputHandler.OnConsumableInventoryOpen += SetConsumableOpen;
        InputHandler.OnKeyItemInventoryOpen += SetKeyItemOpen;
        InputHandler.OnPuzzleJournalOpen += SetPuzzleJournalOpen;
        InputHandler.OnJournalOpen += SetJournalOpen;
    }
    private void OnDisable()
    {
        InputHandler.OnConsumableInventoryOpen -= SetConsumableOpen;
        InputHandler.OnKeyItemInventoryOpen -= SetKeyItemOpen;
        InputHandler.OnPuzzleJournalOpen -= SetPuzzleJournalOpen;
        InputHandler.OnJournalOpen -= SetJournalOpen;
    }

    private void SetConsumableOpen(bool isOpen)
    {
        consumableInventory.SetActive(isOpen);
    }
    private void SetKeyItemOpen(bool isOpen)
    {
        keyItemInventory.SetActive(isOpen);
    }
    private void SetPuzzleJournalOpen(bool isOpen)
    {
        puzzleJournal.SetActive(isOpen);
    }
    private void SetJournalOpen(bool isOpen)
    {
        journal.SetActive(isOpen);
    }
}
