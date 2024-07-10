using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChemicalMixingEventTrigger : MonoBehaviour
{

    public UnityEvent chemicalMixingEventInitiate;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Activated Event!");
        chemicalMixingEventInitiate.Invoke(); //LightManager
    }

    public void ResetIngredients()
    {
        MixingIngredient[] ingredients = FindObjectsOfType<MixingIngredient>();
        foreach (var ingredient in ingredients)
        {
            ingredient.ResetIngredient();
        }
    }
}
