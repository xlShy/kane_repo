using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorKeyItem : InventoryItem
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

    [SerializeField]
    public LayerMask newLayerMask;

    private Coroutine dialogueCoroutine;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        interactableScript.itemPickedUp.AddListener(uponItemPickup);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void uponItemPickup()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
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

    public override void Use()
    {
        Debug.Log("Placed for the sake of placing");
    }
}

