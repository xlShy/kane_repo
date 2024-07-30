using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrandfatherClock : InteractableObject
{
    public GameObject keyItem;
    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;

    public GameObject puzzleCanvas;

    [SerializeField] public ClockChecker clockChecker;
    [SerializeField] private DialogueTriggerScript onPuzzleSolve;

    [SerializeField] private AudioSource puzzleCompleteSounds;

    public UnityEvent OnCompleteGrandfathersClock;

    public bool isPuzzleComplete = false;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && !isPuzzleComplete)
        {
            puzzleCanvas.SetActive(true);
            clockChecker = FindObjectOfType<ClockChecker>();

            if (clockChecker != null)
            {
                clockChecker.onPuzzleCompleted.AddListener(OnPuzzleCompleted);
            }
        }
    }
    private void OnPuzzleCompleted()
    {
        if (!isPuzzleComplete)
        {
            isPuzzleComplete = true;
            OnCompleteGrandfathersClock?.Invoke();
            //Add puzzle to completed in the level1
            pEventHandler.InteractPuzzle(puzzle);

            keyItem.SetActive(true);
            
            keyItem.GetComponent<DoorKeyItem>().PlayKeyDropAudio();
            puzzleCompleteSounds.Play();
            onPuzzleSolve.TriggerDialogue();
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");

            // Remove the listener to prevent multiple invocations
            if (clockChecker != null)
            {
                clockChecker.onPuzzleCompleted.RemoveListener(OnPuzzleCompleted);
            }        
        }
    }
}