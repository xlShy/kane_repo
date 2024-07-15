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
    private string doorId;

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
    [SerializeField] private AudioSource doorisClosed;

    [SerializeField] private Collider objCollider;
    [SerializeField] private Renderer objRenderer;

    private Coroutine dialogueCoroutine;
    private Inventory currentInventory;

    [SerializeField]private GameObject doorAnchor;
    private int keyCount;
    public bool isOpen = false;
    public bool isUnlocked = false;

    //Door Rotation
    [SerializeField] private float openSpeed = 5f;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isRotating = false;
    public List<InventoryItem> keyItems;

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
            if (isUnlocked && !isOpen)
            {

                StartCoroutine(OpenDoor());
                isOpen = true;
            }
            else if (isUnlocked && isOpen)
            {

                StartCoroutine(CloseDoor());
                isOpen = false;
            }
            else
            {
                keyItems = inventory.GetKeyItems();
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
        if (!isUnlocked)
        {
            DoorKeyItem correctKey = keyItems.OfType<DoorKeyItem>().FirstOrDefault(k => k.doorId == this.doorId);
            if (correctKey == null)
            {
                doorIsLocked.Play();
                noKey.TriggerDialogue();
            }
            else
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
        doorisOpen.Play();
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
        doorisClosed.Play();
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
