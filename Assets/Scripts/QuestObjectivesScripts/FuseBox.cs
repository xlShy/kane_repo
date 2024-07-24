using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FuseBox : InteractableObject
{
    [SerializeField] public CameraFlash cameraFlash;

    [SerializeField] private Puzzle puzzle;
    [SerializeField] private PuzzleEventHandler pEventHandler;
    [SerializeField] private KeyItemInventory keyItemInventory;

    [Header("Objects")]
    [SerializeField] private InventoryItem requiredFuse;
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

    //events
    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;
    public UnityEvent fuseBoxActivate; //enables television & enables light

    [HideInInspector]    
    public bool isCompleted = false; //determines if fusebox is solved
    private List<InventoryItem> keyItems; //stores all the fuse inside this list from the inventory

    //fuse count settings
    private int itemCount = 0;
    private int currentCount = 0;

    [Header("Fuse Objective")]
    [SerializeField] private GameObject fuse1;
    [SerializeField] private GameObject fuse2;
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            keyItems = inventory.GetKeyItems(requiredFuse);
            if (keyItems == null)
            {
                return;
            }
            int fuseCount = CountFuseItems(keyItems);
            HandleFuses(fuseCount);
        }
    }
    private int CountFuseItems(List<InventoryItem> items)
    {
        if(itemCount == 0)
        {
            itemCount = items.Count(item => item is FuseItem);
        }
        else
        {
            currentCount = items.Count(item => item is FuseItem);
            itemCount += currentCount;
        }
        return itemCount;
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
            keyItemInventory.RemoveKeyItem(requiredFuse, 1);

            if (fuse1.activeSelf)
            {
                fuse2.SetActive(true);
            }
            fuse1.SetActive(true);
        }
        else if (fuseCount == 2)
        {
            keyItemInventory.RemoveKeyItem(requiredFuse, 2);
            fuseBoxActivate.Invoke(); // TV Script
            SolvePuzzle();      
            StartCoroutine(PlayFuseActivateSounds());

            fuse1.SetActive(true); 
            fuse2.SetActive(true);
            
        }
    }
    private IEnumerator PlayFuseActivateSounds()
    {
        placingFuse.Play();
        fuseActivate.Play();
        fuseActivateSecondPhase.Play();

        yield return new WaitForSeconds(fuseActivateSecondPhase.clip.length - 0.5f);

        //Debug.Log("Now playing sound.");
        fuseLoopSound.Play();
    }
    private void SolvePuzzle()
    {
        cameraFlash.TriggerFlash(1f);
        isCompleted = true;
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        televisionGameObject.layer = LayerMask.NameToLayer("interactableMask");
        successFuseDialogue.TriggerDialogue();

        //set puzzle as complete
        pEventHandler.InteractPuzzle(puzzle);
    }
    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
