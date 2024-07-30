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

    [SerializeField] private string[] requiredItems = { "Dish Soap", "Baking Soda", "Baking Soda" };
    [SerializeField] private InventoryItem deRustingMixture;
    [SerializeField] private GameObject deRustBucket;
    [SerializeField] private AudioSource wrongMixture;
    [SerializeField] private AudioSource correctMixture;
    [SerializeField] private InventoryItem emptyMug;
    public UnityEvent resetInteractableState;
    public UnityEvent OnCompleteChemicalMixing;
    [SerializeField] private GameObject bucketContentsIndicator;

    
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

            UpdateBucketContentsIndicator();
        }

        else if (inventory.HasEmptyMug())
        {
            if (currentMixture.Count > 0)
            {
                Debug.Log("Checking mixture: " + string.Join(", ", currentMixture));
                bool isCorrectMixture = CheckMixture();

                InventoryItem mugItem = inventory.GetKeyItem("Mug");
                if (mugItem != null)
                {
                    mugItem.itemName = "Mug with Mixture";
                    mugItem.itemDescription = "A mug filled with a mixture.";
                    mugItem.isCorrectMixture = isCorrectMixture;

                    inventory.UpdateKeyItem(mugItem);

                    if (isCorrectMixture)
                    {
                        OnCompleteChemicalMixing.Invoke();
                        pEventHandler.InteractPuzzle(puzzle);
                        correctMixture.Play();
                        isPuzzleSolved = true;
                        deRustBucket.SetActive(true);
                        Debug.Log("Correct Mixture Collected!");
                    }
                    else
                    {
                        wrongMixture.Play();
                        Debug.Log("Incorrect mixture collected");
                    }

                    currentMixture.Clear();
                    UpdateBucketContentsIndicator();
                }
            }
            else
            {
                Debug.Log("The bucket is empty. Add ingredients first!");
            }
        }
        else
        {
            Debug.Log("You need a mug to interact with the mixing bucket!");
        }
    }


    private bool CheckMixture()
    {
        if (currentMixture.Count != requiredItems.Length)
        {
            Debug.Log($"Mixture count mismatch. Required: {requiredItems.Length}, Current: {currentMixture.Count}");
            return false;
        }

        Dictionary<string, int> requiredCounts = new Dictionary<string, int>();
        Dictionary<string, int> currentCounts = new Dictionary<string, int>();

        foreach (string item in requiredItems)
        {
            if (!requiredCounts.ContainsKey(item))
                requiredCounts[item] = 1;
            else
                requiredCounts[item]++;
        }

        foreach (string item in currentMixture)
        {
            if (!currentCounts.ContainsKey(item))
                currentCounts[item] = 1;
            else
                currentCounts[item]++;
        }

        Debug.Log("Required mixture: " + string.Join(", ", requiredItems));
        Debug.Log("Current mixture: " + string.Join(", ", currentMixture));

        foreach (var kvp in requiredCounts)
        {
            if (!currentCounts.ContainsKey(kvp.Key) || currentCounts[kvp.Key] != kvp.Value)
            {
                Debug.Log($"Mismatch for {kvp.Key}. Required: {kvp.Value}, Current: {(currentCounts.ContainsKey(kvp.Key) ? currentCounts[kvp.Key] : 0)}");
                return false;
            }
        }

        return true;
    }

    private void ResetChemicalPuzzle()
    {
        currentMixture.Clear();
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
        UpdateBucketContentsIndicator();
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

    private void UpdateBucketContentsIndicator()
    {
        if (bucketContentsIndicator != null)
        {
            bucketContentsIndicator.SetActive(currentMixture.Count > 0);
        }
    }
}
