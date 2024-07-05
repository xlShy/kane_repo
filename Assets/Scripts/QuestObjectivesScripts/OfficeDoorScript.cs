using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OfficeDoorScript : InteractableObject
{
    public KeyItemInventory keyItemInventory;

    private Renderer objectRenderer;

    [SerializeField]
    public int reqNumber;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    private DialogueTriggerScript noKey;

    [SerializeField]
    private DialogueTriggerScript yesKey;

    [SerializeField] private AudioSource doorIsLocked;
    [SerializeField] private AudioSource doorisOpen;

    [SerializeField] private Collider objCollider;
    [SerializeField] private Renderer objRenderer;

    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;
    private int keyCount;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            List<InventoryItem> keyItems = inventory.GetKeyItems();
            keyCount = CountDoorKeyItem(keyItems);
            HandleKey(keyCount);
        }
    }
    private int CountDoorKeyItem(List<InventoryItem> items)
    {
        return items.Count(item => item is DoorKeyItem);
    }

    private void HandleKey(int fuseCount)
    {
        if (keyCount == 0)
        {
            doorIsLocked.Play();
            noKey.TriggerDialogue();
        }
        else if (keyCount == 1)
        {
            objCollider.enabled = false;
            objRenderer.enabled = false;
            doorisOpen.Play();
            yesKey.TriggerDialogue();
            keyItemInventory.RemoveKeyItem(item, 1);
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
