using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillsItem : InventoryItem
{
    [SerializeField]
    public InteractableObject interactableScript;


    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private string dialogueContent;

    [SerializeField]
    private float dialogueDuration;

    private Coroutine dialogueCoroutine;
    private MeshRenderer meshRenderer;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        interactableScript.itemPickedUp.AddListener(dialogueActivate);
        capsuleCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void dialogueActivate()
    {
        Debug.Log("I am called to make dialogue!");
        capsuleCollider.enabled = false;
        meshRenderer.enabled = false;

        dialogueText.gameObject.SetActive(true);
        dialogueText.text = dialogueContent;

        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
        dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(dialogueDuration));
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueText.gameObject.SetActive(false);
    }
}
