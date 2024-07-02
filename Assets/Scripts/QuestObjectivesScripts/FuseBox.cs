using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            int fuseCount = inventory.GetItemCount<FuseItem>();
            HandleFuses(fuseCount);
        }
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
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
            objectRenderer.material.color = Color.green;
            successFuseDialogue.TriggerDialogue();
        }
    }
    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
