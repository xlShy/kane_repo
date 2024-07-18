using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FuseBox : InteractableObject
{
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private PuzzleEventHandler pEventHandler;
    [SerializeField] private KeyItemInventory keyItemInventory;
    [SerializeField] private TVScript tvScript;
    [SerializeField] private LightManager lightManager;

    [Header("Objects")]
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject televisionGameObject;

    [Header("DialogueComponents")]
    [SerializeField] private DialogueTriggerScript successFuseDialogue;
    [SerializeField] private DialogueTriggerScript notEnoughFusesDialogue;
    [SerializeField] private DialogueTriggerScript oneFuseDialogue;

    [Header("SFX")]
    [SerializeField] private AudioSource placingFuse;
    [SerializeField] private AudioSource fuseActivate;
    [SerializeField] private AudioSource fuseActivateSecondPhase;
    [SerializeField] private AudioSource fuseLoopSound;

    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;
    public UnityEvent fuseBoxActivate;

    public bool isCompleted = false;
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        //checks if item is fuse, else return
        if (itemInteractedCase == 2)
        {
            List<InventoryItem> keyItems = inventory.GetKeyItems();
            int fuseCount = CountFuseItems(keyItems);
            HandleFuses(fuseCount);
        }
    }
    private int CountFuseItems(List<InventoryItem> items)
    {
        return items.Count(item => item is FuseItem);
    }
    private void HandleFuses(int fuseCount)
    {
        if (fuseCount == 0)
        {
            notEnoughFusesDialogue.TriggerDialogue();
        }
        else if (fuseCount == 1)
        {
            placingFuse.Play();
            oneFuseDialogue.TriggerDialogue();           
            keyItemInventory.RemoveKeyItem(item, 1);
        }
        else if (fuseCount == 2)
        {
            fuseBoxActivate.Invoke(); // TV Script
            keyItemInventory.RemoveKeyItem(item, 2);
            SolvePuzzle();      
            StartCoroutine(PlayFuseActivateSounds());
            
        }
    }
    private IEnumerator PlayFuseActivateSounds()
    {
        placingFuse.Play();
        fuseActivate.Play();
        fuseActivateSecondPhase.Play();

        yield return new WaitForSeconds(fuseActivateSecondPhase.clip.length - 0.5f);

        Debug.Log("Now playing sound.");
        fuseLoopSound.Play();
    }
    private void SolvePuzzle()
    {
        isCompleted = true;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        televisionGameObject.layer = LayerMask.NameToLayer("interactableMask");
        successFuseDialogue.TriggerDialogue();
        lightManager.TurnOnAll();

        //set puzzle as complete
        pEventHandler.InteractPuzzle(puzzle);
    }
    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
