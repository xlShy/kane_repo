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

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    private DialogueTriggerScript onPuzzleSuccess;

    //refactor
    public UnityEvent onLockPuzzleCompletion;
    public static event Action<Puzzle> OnPuzzleComplete;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            combinationCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            combinationLockScript = FindObjectOfType<CombinationLockScript>();
            if (combinationLockScript != null)
            {
                combinationLockScript.onCorrectCombinationEntered.AddListener(OnCorrectCombinationEntered);
            }
        }
    }

    private void OnCorrectCombinationEntered()
    {
        onPuzzleSuccess.TriggerDialogue();
        combinationCanvas.SetActive(false);
        objectRenderer.material.color = Color.green;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        onLockPuzzleCompletion.Invoke();

        //set puzzle as complete
        pEventHandler.InteractPuzzle(puzzle);
        OnPuzzleComplete?.Invoke(puzzle);
    }
}
