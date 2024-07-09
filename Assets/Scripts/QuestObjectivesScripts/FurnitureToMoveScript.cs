using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FurnitureToMoveScript : InteractableObject
{
    [SerializeField]private CombinationLockActivateScript combinationLockActivate;

    private void Start()
    {
        combinationLockActivate.onLockPuzzleCompletion.AddListener(canBeMoved);
    }

    private void canBeMoved()
    {
        Debug.Log("I am called to change the state of the furniture");
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            transform.localPosition = new Vector3(15, 13, 29);
            gameObject.layer = LayerMask.NameToLayer("solvedPuzzle");
        }

    }
}
