using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrandfatherClock : InteractableObject
{
    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;

    public GameObject puzzleCanvas;
    [SerializeField]
    public LayerMask newLayerMask;

    //[SerializeField]
    //public GameObject doorToOpen;

    [SerializeField]
    public ClockChecker clockChecker;

    [SerializeField]
    private DialogueTriggerScript onPuzzleSolve;

    public UnityEvent doorOpen;
    public static event Action<Puzzle> OnPuzzleComplete;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            puzzleCanvas.SetActive(true);
            clockChecker = FindObjectOfType<ClockChecker>();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

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
        doorOpen.Invoke();
        onPuzzleSolve.TriggerDialogue();
        Debug.Log("Puzzle completed! Deactivating canvas.");
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //doorToOpen.SetActive(false);

        //set puzzle as complete
        pEventHandler.InteractPuzzle(puzzle);
        OnPuzzleComplete?.Invoke(puzzle);
    }
}