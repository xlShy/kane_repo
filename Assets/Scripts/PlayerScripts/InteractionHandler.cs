using UnityEngine;


public class InteractionHandler : MonoBehaviour
{

    public ShowInteractableUI showInteractableUI;

    private IInteractable interactableObj;
    private InteractableConfig interactable;

    [Header("Interactable Range")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 0.5f;  // Increased for demo purposes
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private int itemInteractedCase = 0;
    private Inventory inventory;
    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableMask);

        if (numFound > 0f)
        {
            interactableObj = colliders[0].GetComponent<IInteractable>();

            if (interactableObj != null)
            {
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
                        case InteractableType.Pickup:
                            Debug.Log("You picked me up!");
                            itemInteractedCase = 1;
                            break;
                        case InteractableType.Objective:
                            Debug.Log("This is an objective!");
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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
