using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerScript : MonoBehaviour
{
    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private List<string> dialogueContent;

    [SerializeField]
    private float dialogueDuration;

    [SerializeField]
    private bool isRepeating;

    private bool alreadyActivated = false;
    private static Queue<DialogueInfo> dialogueQueue = new Queue<DialogueInfo>();
    private static Coroutine dialogueCoroutine;
    private int currentDialogueIndex = 0;

    private class DialogueInfo
    {
        public List<string> Content;
        public float Duration;
        public bool IsRepeating;
        public DialogueTriggerScript Trigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyActivated && dialogueContent.Count > 0)
        {
            EnqueueDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isRepeating)
        {
            alreadyActivated = true;
        }
    }

    private void EnqueueDialogue()
    {
        DialogueInfo info = new DialogueInfo
        {
            Content = new List<string>(dialogueContent),
            Duration = dialogueDuration,
            IsRepeating = isRepeating,
            Trigger = this
        };

        dialogueQueue.Enqueue(info);

        if (dialogueCoroutine == null)
        {
            dialogueCoroutine = StartCoroutine(ProcessDialogueQueue());
        }
    }

    private static IEnumerator ProcessDialogueQueue()
    {
        while (dialogueQueue.Count > 0)
        {
            DialogueInfo currentDialogue = dialogueQueue.Dequeue();
            yield return currentDialogue.Trigger.DisplayAllDialogues(currentDialogue);
        }
        dialogueCoroutine = null;
    }

    private IEnumerator DisplayAllDialogues(DialogueInfo info)
    {
        for (int i = 0; i < dialogueContent.Count; i++)
        {
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = dialogueContent[i];

            yield return new WaitForSeconds(dialogueDuration);
            dialogueText.gameObject.SetActive(false);
        }

        if (isRepeating)
        {
            alreadyActivated = false;
        }
    }
}
