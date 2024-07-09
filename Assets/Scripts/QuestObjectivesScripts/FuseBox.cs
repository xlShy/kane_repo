using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FuseBox : InteractableObject
{
    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;
    public KeyItemInventory keyItemInventory;
    public bool isCompleted = false;

    private Renderer objectRenderer;

    [SerializeField]
    public int reqNumber;

    [SerializeField]
    private Text dialogueText;

    [SerializeField] public GameObject televisionGameObject;

    [SerializeField]
    private DialogueTriggerScript successFuseDialogue;
    [SerializeField]
    private DialogueTriggerScript notEnoughFusesDialogue;
    [SerializeField]
    private DialogueTriggerScript oneFuseDialogue;

    [SerializeField]
    private AudioSource placingFuse;

    [SerializeField]
    private AudioSource fuseActivate;

    [SerializeField]
    private AudioSource fuseActivateSecondPhase;

    [SerializeField]
    private AudioSource fuseLoopSound;

    [SerializeField] private TVScript tvScript;

    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
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
        //RemoveFusesFromInventory(2);

        //set puzzle as complete
        pEventHandler.InteractPuzzle(puzzle);
    }
    //TO DO - edit to remove fuses per interact
    private void RemoveFusesFromInventory(int count)
    {
        if (currentInventory != null)
        {
            List<InventoryItem> keyItems = currentInventory.GetKeyItems();
            List<FuseItem> fusesToRemove = keyItems.OfType<FuseItem>().Take(count).ToList();

            foreach (FuseItem fuse in fusesToRemove)
            {
                keyItems.Remove(fuse);
            }
        }
    }
    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
