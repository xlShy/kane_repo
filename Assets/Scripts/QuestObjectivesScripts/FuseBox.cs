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
    private string dialogueContent;

    [SerializeField]
    private float dialogueDuration;

    [SerializeField]
    public LayerMask newLayerMask;

    private Coroutine dialogueCoroutine;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == reqNumber)
        {
            if (inventory.GetItemCount<FuseItem>() >= reqNumber)
            {
                Debug.Log("You have enough fuses!");
                objectRenderer.material.color = Color.green;
                gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");

                dialogueText.gameObject.SetActive(true);
                dialogueText.text = dialogueContent;

                if (dialogueCoroutine != null)
                {
                    StopCoroutine(dialogueCoroutine);
                }
                dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(dialogueDuration));
            }
            else
            {
                Debug.Log("You do not have enough!");
            }
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
