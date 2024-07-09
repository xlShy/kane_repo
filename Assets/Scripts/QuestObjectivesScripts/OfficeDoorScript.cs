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

    [SerializeField]private GameObject doorAnchor;
    private int keyCount;
    private bool isOpen = false;
    private bool isUnlocked = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            if (isUnlocked && !isOpen)
            {
                doorisOpen.Play();
                doorAnchor.transform.rotation = Quaternion.Euler(0, 90, 0);
                isOpen = true;
            }
            else if (isUnlocked && isOpen)
            {
                doorisOpen.Play();
                doorAnchor.transform.rotation = Quaternion.Euler(0, 0, 0);
                isOpen = false;
            }
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
        if (!isUnlocked)
        {
            if (keyCount == 0)
            {
                doorIsLocked.Play();
                noKey.TriggerDialogue();
            }
            else if (keyCount == 1)
            {
                doorAnchor.transform.rotation = Quaternion.Euler(0, 90, 0);
                doorisOpen.Play();
                yesKey.TriggerDialogue();
                keyItemInventory.RemoveKeyItem(item, 1);
                isUnlocked = true;
            }
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
