
using UnityEngine;
using UnityEngine.Events;

public class ChemicalMixingEventTrigger : MonoBehaviour
{
    public bool onlyTriggerOnce = false;

    public UnityEvent chemicalEventStart;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyTriggerOnce && isTriggered)
        {
            return;
        }
        //locks bathroom and office2bathroom door
        //starts the puzzle
        chemicalEventStart.Invoke();
        isTriggered = true;
    }
}
