using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class InteractionHandler : MonoBehaviour
{

    public ShowInteractableUI showInteractableUI;
    private IInteractable interactableObj;
    private InteractableConfig interactable;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private List<GameObject> uiCanvas;
    [SerializeField] private Transform cameraTransform;

    // OLD INTERACTION SYSTEM 
    //[Header("Interactable Range")]
    //[SerializeField] private Transform interactionPoint;
    //[SerializeField] private float interactionRadius = 0.5f;  // Increased for demo purposes
    //[SerializeField] private LayerMask interactableMask;
    //[SerializeField] private List<GameObject> uiCanvas;
    //private readonly Collider[] colliders = new Collider[3];
    
    private int itemInteractedCase = 0;
    private Inventory inventory;
    public UnityEvent isInteracting;
    public UnityEvent isNotInteracting;

    public bool isCanvasEnabled;

    private void OnEnable()
    {
        CanvasManager.OnCanvasEnabled += isAnyCanvasOn;
    }
    private void OnDisable()
    {
        CanvasManager.OnCanvasEnabled -= isAnyCanvasOn;
    }
    private void Start()
    {
        inventory = GetComponent<Inventory>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    
    void Update()
    {
        Interact();
    }
    public void Interact()
    {
        // OLD INTERACTION SYSTEM
        //numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableMask);
        //if (numFound > 0f && !IsAnyCanvasActive())
        //{
        //    interactableObj = colliders[0].GetComponent<IInteractable>();
        //    if (interactableObj != null)
        //    {
        //        isInteracting.Invoke();
        //        interactable = interactableObj.GetInteractableConfig();
        //        showInteractableUI.SetInteractionPrompt(interactable.promptImage);
        //        showInteractableUI.EnableInteractableUI();
        //        if (Input.GetKeyDown(KeyCode.F))
        //        {
        //            switch (interactable.interactableType)
        //            {
        //                case InteractableType.Interactable:
        //                    Debug.Log("You interacted with me!");
        //                    itemInteractedCase = 0;
        //                    break;
        //                case InteractableType.PickUp:
        //                    //Debug.Log("You picked up an item!");
        //                    itemInteractedCase = 1;
        //                    break;
        //                case InteractableType.Objective:
        //                    Debug.Log("This is an objective!");
        //                    itemInteractedCase = 2;
        //                    break;
        //                case InteractableType.PickupnoDestroy:
        //                    Debug.Log("You picked me up! will not destroy.");
        //                    itemInteractedCase = 3;
        //                    break;
        //            }
        //            interactableObj.Interact(itemInteractedCase, inventory);
        //        }
        //    }
        //}

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionDistance, interactableMask))
        {
            interactableObj = hit.collider.GetComponent<IInteractable>();

            //if (interactableObj != null && !IsAnyCanvasActive())
            if(interactableObj != null && !isCanvasEnabled) 
            {
                isInteracting.Invoke();
                interactable = interactableObj.GetInteractableConfig();
                showInteractableUI.SetInteractionPrompt(interactable.promptImage);
                showInteractableUI.EnableInteractableUI();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    switch (interactable.interactableType)
                    {
                        case InteractableType.Interactable:
                            Debug.Log("You interacted with me!");
                            itemInteractedCase = 0;
                            break;
                        case InteractableType.PickUp:
                            //Debug.Log("You picked up an item!");
                            itemInteractedCase = 1;
                            break;
                        case InteractableType.Objective:
                            //Debug.Log("This is an objective!");
                            itemInteractedCase = 2;
                            break;
                        case InteractableType.PickupnoDestroy:
                            Debug.Log("You picked me up! will not destroy.");
                            itemInteractedCase = 3;
                            break;
                    }
                    interactableObj.Interact(itemInteractedCase, inventory);
                }
            }
        }
        else
        {
            showInteractableUI.DisableInteractableUI();
            isNotInteracting.Invoke();
        }
    }
    public void isAnyCanvasOn(bool isOn)
    {
        isCanvasEnabled = isOn;
    }
    private void OnDrawGizmos()
    {
        if (cameraTransform != null)
        {
            // OLD INTERACTION SYSTEM
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactionDistance);
        }
    }
}
