using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTriggerScript : MonoBehaviour
{
    [TextArea(5, 7)]
    [SerializeField] private List<string> dialogueContent;
    [SerializeField] private float dialogueDuration;
    [SerializeField] private bool isRepeating;
    [SerializeField] private bool isInteractDialogue;
    private bool hasPlayedOnce = false;
    public UnityEvent OnTriggerDialogueAudio;

    public void TriggerDialogue()
    {
        OnTriggerDialogueAudio?.Invoke();
        if ((!hasPlayedOnce || isRepeating) && dialogueContent.Count > 0)
        {
            bool started = DialogueManager.instance.StartDialogue(dialogueContent, dialogueDuration, isInteractDialogue);
            if (started)
            {
                hasPlayedOnce = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!hasPlayedOnce || isRepeating) && (!DialogueManager.instance.isDisplayingDialogue || isInteractDialogue))
        {
            
            TriggerDialogue();
        }
    }

    public void TriggerRandomDialogue()
    {
        if ((!hasPlayedOnce || isRepeating) && dialogueContent.Count > 0)
        {
            int randomIndex = Random.Range(0, dialogueContent.Count);
            string randomContent = dialogueContent[randomIndex];
            bool started = DialogueManager.instance.StartDialogue(new List<string> { randomContent }, dialogueDuration, isInteractDialogue);
            if (started)
            {
                hasPlayedOnce = true;
            }
        }
    }

    // Optional: Method to reset the trigger if needed (e.g., for scene changes or specific game events)
    public void ResetTrigger()
    {
        hasPlayedOnce = false;
    }
}