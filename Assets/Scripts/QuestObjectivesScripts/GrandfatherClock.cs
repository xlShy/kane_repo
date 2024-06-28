using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherClock : InteractableObject
{

    public GameObject puzzleCanvas;
    private Renderer objectRenderer;
    [SerializeField]
    public LayerMask newLayerMask;

    //[SerializeField]
    //public GameObject doorToOpen;

    [SerializeField]
    public ClockChecker clockChecker;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            puzzleCanvas.SetActive(true);
            clockChecker = FindObjectOfType<ClockChecker>();

            if (clockChecker != null)
            {
                clockChecker.onPuzzleCompleted.AddListener(OnPuzzleCompleted);
            }
            else
            {
                Debug.Log("Not found!");
            }
        }
    }

    void ObjectiveOutline()
    {
        //this will be dedicated to outline of the objective until 1st time interact
    }
    private void OnPuzzleCompleted()
    {
        Debug.Log("Puzzle completed! Deactivating canvas.");
        objectRenderer.material.color = Color.green;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        //doorToOpen.SetActive(false);
    }
}