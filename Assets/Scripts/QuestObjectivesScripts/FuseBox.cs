using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FuseBox : InteractableObject
{
    private Renderer objectRenderer;

    [SerializeField]
    public int reqNumber;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    private DialogueTriggerScript successFuseDialogue;
    [SerializeField]
    private DialogueTriggerScript notEnoughFusesDialogue;
    [SerializeField]
    private DialogueTriggerScript oneFuseDialogue;

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
        Debug.Log(fuseCount);
        if (fuseCount == 0)
        {
            notEnoughFusesDialogue.TriggerDialogue();
        }
        else if (fuseCount == 1)
        {
            oneFuseDialogue.TriggerDialogue();
        }
        else if (fuseCount == 2)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        objectRenderer.material.color = Color.green;
        successFuseDialogue.TriggerDialogue();
        RemoveFusesFromInventory(2);
    }

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
