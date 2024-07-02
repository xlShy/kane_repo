using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOnClose : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private DialogueTriggerScript onCloseInterface;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
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
