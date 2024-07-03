using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseItem : InventoryItem
{
    [SerializeField]
    public InteractableObject interactableScript;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private string dialogueContent;

    [SerializeField]
    private float dialogueDuration;

    [SerializeField]
    private AudioSource fusePickup;

    private Coroutine dialogueCoroutine;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        interactableScript.itemPickedUp.AddListener(uponItemPickup);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void uponItemPickup()
    {
        fusePickup.Play();
        Debug.Log("I am called to make dialogue!");
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = dialogueContent;
        
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
        dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(dialogueDuration));

        meshRenderer.enabled = false;
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueText.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
