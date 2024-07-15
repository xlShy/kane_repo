using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChemicalMixingEventTrigger : MonoBehaviour
{

    public UnityEvent chemicalMixingEventInitiate;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            Debug.Log("Activated Event!");
            chemicalMixingEventInitiate.Invoke(); //LightManager & MixingIngredients
            isTriggered = true;
        }
        
    }
}
