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

    [SerializeField] public CombinationLockScript combinationLockScript;

    [SerializeField] private GameObject readableDocument;

    [SerializeField] private DialogueTriggerScript onPuzzleSuccess;

    private bool canvasWasOpened;

    public UnityEvent OnCompleteCombinationLock;

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
        OnCompleteCombinationLock?.Invoke();
        //Add puzzle to completed in the level1
        pEventHandler.InteractPuzzle(puzzle);

        readableDocument.SetActive(true);
        onPuzzleSuccess.TriggerDialogue();
        combinationCanvas.SetActive(false);
        objectRenderer.material.color = Color.green;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");  
    }
}
