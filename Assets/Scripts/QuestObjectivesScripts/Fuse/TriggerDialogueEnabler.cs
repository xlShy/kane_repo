using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueEnabler : MonoBehaviour
{
    public void EnableTriggerDialogue(GameObject triggerDialogue2Enable)
    {
        triggerDialogue2Enable.SetActive(true);
    }
    public void DisableTriggerDialogue(GameObject triggerDialogue2Enable)
    {
        triggerDialogue2Enable.SetActive(false);
    }
}
