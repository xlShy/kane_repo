using UnityEngine;
using UnityEngine.Events;

public class OnPlayerEnterEventTrigger : MonoBehaviour
{
    public bool onlyTriggerOnce;
    public UnityEvent events;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyTriggerOnce && isTriggered)
            return;

        Debug.Log("Activated Event!");
        events.Invoke(); //MixingIngredients
        isTriggered = true;
    }
}
