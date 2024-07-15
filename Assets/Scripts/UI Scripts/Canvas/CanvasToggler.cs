using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggler : MonoBehaviour
{
    [Header("Player UI Canvas")]
    [SerializeField] protected GameObject keyItemInventory;
    [SerializeField] protected GameObject consumableInventory;
    [SerializeField] protected GameObject journal;

    [Header("Puzzle UI Canvas")]
    [SerializeField] protected GameObject grandFathersClock;
    [SerializeField] protected GameObject combinationLock;
    public void OnEnable()
    {
        InputHandler.OnConsumableInventoryOpen += SetConsumableOpen;
        InputHandler.OnKeyItemInventoryOpen += SetKeyItemOpen;
        InputHandler.OnJournalOpen += SetJournalOpen;
    }
    private void OnDisable()
    {
        InputHandler.OnConsumableInventoryOpen -= SetConsumableOpen;
        InputHandler.OnKeyItemInventoryOpen -= SetKeyItemOpen;
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
    private void SetJournalOpen(bool isOpen)
    {
        journal.SetActive(isOpen);
    }
}
