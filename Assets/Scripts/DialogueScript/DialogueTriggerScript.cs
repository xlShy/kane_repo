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
    public bool alreadyActivated = false;
    public UnityEvent OnTriggerDialogueAudio;

    public void TriggerDialogue()
    {
        if (!alreadyActivated && dialogueContent.Count > 0)
        {
            bool started = DialogueManager.instance.StartDialogue(dialogueContent, dialogueDuration, isInteractDialogue);
            if (started && !isRepeating)
            {
                alreadyActivated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!DialogueManager.instance.isDisplayingDialogue || isInteractDialogue)
        {
            OnTriggerDialogueAudio?.Invoke();
            TriggerDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isRepeating)
        {
            alreadyActivated = false;  // Reset this so it can be triggered again in the future
        }
    }

    public void TriggerRandomDialogue()
    {
        if (dialogueContent.Count > 0)
        {
            int randomIndex = Random.Range(0, dialogueContent.Count);
            string randomContent = dialogueContent[randomIndex];
            DialogueManager.instance.StartDialogue(new List<string> { randomContent }, dialogueDuration, isInteractDialogue);
        }
    }
}