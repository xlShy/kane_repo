using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class CombinationLockActivateScript : InteractableObject
{
    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;

    public GameObject combinationCanvas;
    private Renderer objectRenderer;

    [SerializeField]
    public CombinationLockScript combinationLockScript;

    [SerializeField] private GameObject readableDocument;

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    private DialogueTriggerScript onPuzzleSuccess;

    private bool canvasWasOpened;

    public UnityEvent onLockPuzzleCompletion;
    //journal event
    public static PuzzleStatus.onCompletedEvents OnCombinationLockComplete;
    public bool isPuzzleComplete = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            combinationCanvas.SetActive(true);
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

            combinationLockScript = FindObjectOfType<CombinationLockScript>();
            canvasWasOpened = true;
        }
    }

    public void CheckCombinationOnClose()
    {
        if (canvasWasOpened && combinationLockScript != null)
        {
            combinationLockScript.CheckCombination();
            if (combinationLockScript.isCorrect)
            {
                OnCorrectCombinationEntered();
            }
        }
        canvasWasOpened = false;
    }
    private void OnCorrectCombinationEntered()
    {
        isPuzzleComplete = true;
        readableDocument.SetActive(true);
        onPuzzleSuccess.TriggerDialogue();
        combinationCanvas.SetActive(false);
        objectRenderer.material.color = Color.green;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        onLockPuzzleCompletion.Invoke();

        //Add puzzle to completed in the level1
        pEventHandler.InteractPuzzle(puzzle);
        //Add completed puzzle to journal
        OnCombinationLockComplete?.Invoke();
    }
}
