using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceOnClose : MonoBehaviour
{
    [SerializeField] private GameObject interfaceObject;

    [SerializeField] private DialogueTriggerScript onCloseInterface;

    [SerializeField] private CombinationLockActivateScript combinationLockActivate;
    [SerializeField] private GrandfatherClock grandfatherClock;

    public UnityEvent interfaceClosed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interfaceObject.activeSelf)
        {
            CloseInterface();
        }
    }
    private void CloseInterface()   
    {
        interfaceClosed.Invoke(); // ReadableDocumentScript
        if (onCloseInterface != null && (grandfatherClock == null || !grandfatherClock.isPuzzleComplete) && (combinationLockActivate == null || !combinationLockActivate.isPuzzleComplete))
        {
            Debug.Log("call for dialogue");
            onCloseInterface.TriggerDialogue();
        }
        if (combinationLockActivate != null)
        {
            combinationLockActivate.CheckCombinationOnClose();
        }
        interfaceObject.SetActive(false);
    }
}
