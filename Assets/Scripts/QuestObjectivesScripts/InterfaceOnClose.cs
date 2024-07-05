using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceOnClose : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private DialogueTriggerScript onCloseInterface;

    public UnityEvent interfaceClosed;
    private Image clockBackground;

    private void Start()
    {
        clockBackground = GetComponent<Image>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interfaceClosed.Invoke();
            if (onCloseInterface != null)
            {
                onCloseInterface.TriggerDialogue();
            }
            Debug.Log("ACTIVATE");
            onCloseInterface.TriggerDialogue();
            gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
