using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrandfatherClock : InteractableObject
{
    [SerializeField] public CameraFlash cameraFlash;
    public bool isPuzzleComplete = false;

    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;

    public GameObject puzzleCanvas;
    [SerializeField]
    public LayerMask newLayerMask;


    [SerializeField]
    public ClockChecker clockChecker;

    [SerializeField]
    private DialogueTriggerScript onPuzzleSolve;

    [SerializeField] private AudioSource puzzleCompleteSounds;

    public GameObject keyItem;
    public UnityEvent puzzleComplete;
    
    //journal event
    public static PuzzleStatus.onCompletedEvents OnGrandfathersClockComplete;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && !isPuzzleComplete)
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
        if (!isPuzzleComplete)
        {
            cameraFlash.TriggerFlash(1f);
            isPuzzleComplete = true;

            keyItem.SetActive(true);
            puzzleCompleteSounds.Play();
            puzzleComplete.Invoke();
            onPuzzleSolve.TriggerDialogue();
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");


            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;

            // Remove the listener to prevent multiple invocations
            if (clockChecker != null)
            {
                clockChecker.onPuzzleCompleted.RemoveListener(OnPuzzleCompleted);
            }

            //Add puzzle to completed in the level1
            pEventHandler.InteractPuzzle(puzzle);
            //Add completed puzzle to journal
            OnGrandfathersClockComplete?.Invoke();
        }
    }
}