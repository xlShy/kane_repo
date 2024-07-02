using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInterfaceOnClose : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private DialogueTriggerScript onCloseTVInterface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(false);
            onCloseTVInterface.TriggerDialogue();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
