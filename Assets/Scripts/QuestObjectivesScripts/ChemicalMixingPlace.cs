using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChemicalMixingPlace : InteractableObject
{
    public Puzzle puzzle;
    public PuzzleEventHandler pEventHandler;

    [SerializeField] public Inventory inventory;
    [SerializeField] public KeyItemInventory keyInventory;

    [SerializeField] private string[] requiredItems = { "Dish Soap", "Baking Soda" };
    [SerializeField] private string[] failItems = { "Salt", "Pepper" };
    [SerializeField] private InventoryItem deRustingMixture;
    [SerializeField] private GameObject deRustBucket;
    [SerializeField] private AudioSource wrongMixture;
    [SerializeField] private AudioSource correctMixture;

    public UnityEvent resetInteractableState;
    public UnityEvent OnCompleteChemicalMixing;
    
    private bool isPuzzleSolved = false;

    [SerializeField] private List<GameObject> chemicalIngredients;

    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (isPuzzleSolved)
        {
            gameObject.SetActive(false);

            if (deRustingMixture != null)
            {
                inventory.AddKeyItem(deRustingMixture);
                Debug.Log("Added De-Rusting Mixture to inventory!");
            }
            return;
        }

        bool hasAllRequiredItems = true;
        bool hasFailItem = false;

        // Check for required items
        foreach (string itemName in requiredItems)
        {
            InventoryItem item = inventory.GetItemByName(itemName);
            if (item == null)
            {
                hasAllRequiredItems = false;
                break;
            }
        }

        // Check for fail items
        foreach (string failItemName in failItems)
        {
            InventoryItem failItem = inventory.GetItemByName(failItemName);
            if (failItem != null)
            {
                hasFailItem = true;
                break;
            }
        }

        // Clear the entire key item inventory
        inventory.ClearAllKeyItems();

        // Check puzzle state
        if (hasAllRequiredItems && !hasFailItem)
        {
            OnCompleteChemicalMixing.Invoke();
            //Add puzzle to completed in the level1
            pEventHandler.InteractPuzzle(puzzle);

            correctMixture.Play();
            isPuzzleSolved = true;
            deRustBucket.SetActive(true);      
        }
        else
        {
            wrongMixture.Play();
            if (hasFailItem)
            {
                //insert dialogue
                Debug.Log("Incorrect!");
            }
            else
            {
                //insert dialogue
                Debug.Log("Kulang!");
            }
            ResetChemicalPuzzle();
        }
    }
    private void ResetChemicalPuzzle()
    {
        //insert dialogue
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    } 
    public void StartChemicalEvent()
    {
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }
    public void MakeIngredientsInteractable()
    {
        foreach(GameObject ingredient in chemicalIngredients)
        {
            ingredient.layer = LayerMask.NameToLayer("interactableMask");
        }
    }
}
