using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingIngredient : InventoryItem
{
    private Renderer objectRenderer;

    [SerializeField]
    public bool ingredientState;

    [SerializeField]
    public LayerMask newLayerMask;

    [SerializeField]
    public ChemicalMixingEventTrigger eventScript;

    [SerializeField]
    public ChemicalMixingPlace mixingScript;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        eventScript.chemicalMixingEventInitiate.AddListener(StartPuzzle);
        mixingScript.resetInteractableState.AddListener(ResetIngredient);
    }

    private void StartPuzzle()
    {
        objectRenderer.material.color = Color.red;
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }

    public void ResetIngredient()
    {
        gameObject.layer = LayerMask.NameToLayer("interactableMask");
    }

    public override void Use()
    {
        Debug.Log("Placed for the sake of placing");
    }

}
