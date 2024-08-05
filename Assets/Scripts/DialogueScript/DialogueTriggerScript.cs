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
    public bool alreadyActivated = false;
    public UnityEvent OnTriggerDialogueAudio;

    public void TriggerDialogue()
    {
        if (!alreadyActivated && dialogueContent.Count > 0)
        {
            bool enqueued = DialogueManager.Instance.EnqueueDialogue(dialogueContent, dialogueDuration);
            if (enqueued && !isRepeating)
            {
                alreadyActivated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerDialogueAudio?.Invoke();
        TriggerDialogue();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isRepeating)
        {
            alreadyActivated = true;
        }
    }

    public void TriggerRandomDialogue()
    {
        if (dialogueContent.Count > 0)
        {
            int randomIndex = Random.Range(0, dialogueContent.Count);
            string randomContent = dialogueContent[randomIndex];
            DialogueManager.Instance.EnqueueDialogue(new List<string> { randomContent }, dialogueDuration);
        }
    }
}