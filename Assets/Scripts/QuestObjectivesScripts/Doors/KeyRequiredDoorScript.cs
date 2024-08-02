using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class KeyRequiredDoorScript : InteractableObject
{
    [SerializeField] private KeyItemInventory keyItemInventory;
    [SerializeField] private InventoryItem requiredKey;

    [SerializeField] private DoorBase doorBase;
    [SerializeField] private string doorId;
    [SerializeField] private Text dialogueText;

    [SerializeField] private DialogueTriggerScript noKey;
    [SerializeField] private DialogueTriggerScript yesKey;

    [SerializeField] private float openSpeed = 5f;

    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;

    private bool isUnlocked = false;

    public List<InventoryItem> keyItems;

    public UnityEvent OnTriggerAudioDialogue;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            if (isUnlocked && !doorBase.isOpen && doorBase.canOpen)
            {
                doorBase.OpenDoor(openSpeed);
                doorBase.isOpen = true;
            }
            else if (isUnlocked && doorBase.isOpen && doorBase.canOpen)
            {
                doorBase.CloseDoor(openSpeed);
                doorBase.isOpen = false;

            }
            else
            {
                keyItems = inventory.GetKeyItems(requiredKey);
                HandleKey(keyItems);
            }
        }
    }
    private int CountDoorKeyItem(List<InventoryItem> items)
    {
        return items.Count(item => item is DoorKeyItem);
    }

    private void HandleKey(List<InventoryItem> keyItems)
    {
        if (keyItems == null || !isUnlocked)
        {
            DoorKeyItem correctKey = keyItems?.OfType<DoorKeyItem>().FirstOrDefault(k => k.doorId == this.doorId);
            if (correctKey == null)
            {
                if (doorBase != null && doorBase.doorIsLocked != null)
                {
                    doorBase.doorIsLocked.Play();
                }
                noKey.TriggerDialogue();
                OnTriggerAudioDialogue?.Invoke();
            }
            else
            {
                if (doorBase != null && doorBase.doorisOpen != null)
                {
                    doorBase.doorisOpen.Play();
                }
                yesKey?.TriggerDialogue();

                if (keyItemInventory != null)
                {
                    keyItemInventory.RemoveKeyItem(correctKey, 1);
                }
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
