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

    //Door Rotation
    [SerializeField] private float openSpeed = 5f;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool canOpen = true;
    private bool isRotating = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        closedRotation = doorAnchor.transform.rotation;
        openRotation = Quaternion.Euler(0, 90, 0);

    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && !isRotating)
        {
            if (isUnlocked && !isOpen && canOpen)
            {
                doorisOpen.Play();
                StartCoroutine(OpenDoor());
                isOpen = true;
            }
            else if (isUnlocked && isOpen)
            {
                doorisOpen.Play();
                StartCoroutine(CloseDoor());
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
                doorisOpen.Play();
                yesKey.TriggerDialogue();
                keyItemInventory.RemoveKeyItem(item, 1);
                isUnlocked = true;
            }
        }
    }
    private IEnumerator OpenDoor()
    {
        if (isRotating) yield break;
        isRotating = true;

        while (Quaternion.Angle(doorAnchor.transform.rotation, openRotation) > 0.01f)
        {
            doorAnchor.transform.rotation = Quaternion.Lerp(doorAnchor.transform.rotation, openRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        doorAnchor.transform.rotation = openRotation;
        isOpen = true;
        isRotating = false;
    }

    private IEnumerator CloseDoor()
    {
        if (isRotating) yield break;
        isRotating = true;

        while (Quaternion.Angle(doorAnchor.transform.rotation, closedRotation) > 0.01f)
        {
            doorAnchor.transform.rotation = Quaternion.Lerp(doorAnchor.transform.rotation, closedRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        doorAnchor.transform.rotation = closedRotation;
        isOpen = false;
        isRotating = false;
    }
    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogueText.gameObject.SetActive(false);
    }
}
