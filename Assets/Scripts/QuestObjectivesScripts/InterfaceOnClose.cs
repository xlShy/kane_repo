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

    [SerializeField] private CombinationLockActivateScript combinationLockActivate;
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CloseInterface();
        }
    }

    private void CloseInterface()
    {
        interfaceClosed.Invoke();
        if (onCloseInterface != null)
        {
            onCloseInterface.TriggerDialogue();
        }
        if (combinationLockActivate != null)
        {
            combinationLockActivate.CheckCombinationOnClose();
        }

        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}
