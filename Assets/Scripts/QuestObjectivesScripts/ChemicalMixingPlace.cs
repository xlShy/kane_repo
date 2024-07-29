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
    [SerializeField] private InventoryItem deRustingMixture;
    [SerializeField] private GameObject deRustBucket;
    [SerializeField] private AudioSource wrongMixture;
    [SerializeField] private AudioSource correctMixture;
    [SerializeField] private InventoryItem emptyMug;
    public UnityEvent resetInteractableState;
    public UnityEvent OnCompleteChemicalMixing;
    
    private bool isPuzzleSolved = false;

    [SerializeField] private List<GameObject> chemicalIngredients;

    private List<string> currentMixture = new List<string>();

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

        if (inventory.HasFilledMug())
        {
            InventoryItem mugItem = inventory.GetFilledMugItem();
            string ingredientName = mugItem.itemName.Replace("Mug with ", "");
            currentMixture.Add(ingredientName);

            mugItem.itemName = "Mug";
            mugItem.itemDescription = "An empty mug.";
            inventory.UpdateKeyItem(mugItem);

            Debug.Log($"Added {ingredientName} to the mixture. Current mixture: {string.Join(", ", currentMixture)}");
            Debug.Log("Mug is now empty.");
        }

        else if (inventory.HasEmptyMug())
        {
            bool isCorrectMixture = CheckMixture();
            if (isCorrectMixture)
            {
                OnCompleteChemicalMixing.Invoke();
                pEventHandler.InteractPuzzle(puzzle);
                correctMixture.Play();
                isPuzzleSolved = true;
                deRustBucket.SetActive(true);
                Debug.Log("Correct mixture!");
            }
            else
            {
                wrongMixture.Play();
                Debug.Log("Incorrect mixture");
            }
            currentMixture.Clear();
        }
        else
        {
            Debug.Log("You need a mug to interact with the mixing bucket!");
        }
    }


    private bool CheckMixture()
    {
        if (currentMixture.Count != requiredItems.Length)
            return false;

        foreach (string item in requiredItems)
        {
            if (!currentMixture.Contains(item))
                return false;
        }

        return true;
    }
    private void ResetChemicalPuzzle()
    {
        currentMixture.Clear();
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

    public void DrainMixture()
    {
        currentMixture.Clear();
        Debug.Log("Mixing bucket has been drained.");
    }
}
