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
        chemicalMixingEventInitiate.Invoke();
    }
}
