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
    public bool alreadyActivated = false;
    private Queue<DialogueInfo> dialogueQueue = new Queue<DialogueInfo>();
    private Coroutine dialogueCoroutine;
    //private int currentDialogueIndex = 0;

    private class DialogueInfo
    {
        public List<string> Content;
        public float Duration;
        public bool IsRepeating;
        public DialogueTriggerScript Trigger;
    }

    public void TriggerDialogue()
    {
        if (!alreadyActivated && dialogueContent.Count > 0)
        {
            EnqueueDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
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
            //Debug.Log("Starting new dialogue coroutine.");
            dialogueCoroutine = StartCoroutine(ProcessDialogueQueue());
        }
        else
        {
            //Debug.Log("Dialogue coroutine already running. Enqueueing only.");
        }
    }

    private IEnumerator ProcessDialogueQueue()
    {
        while (dialogueQueue.Count > 0)
        {
            DialogueInfo currentDialogue = dialogueQueue.Dequeue();
            yield return DisplayAllDialogues(currentDialogue);
        }
        dialogueCoroutine = null;
    }

    private IEnumerator DisplayAllDialogues(DialogueInfo info)
    {
        //Debug.Log($"Starting to display dialogues. Count: {info.Content.Count}");
        for (int i = 0; i < info.Content.Count; i++)
        {
            //Debug.Log($"Displaying dialogue {i + 1}: {info.Content[i]}");
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = info.Content[i];
            //Debug.Log($"Text component text set to: {dialogueText.text}");
            yield return new WaitForSeconds(info.Duration);
            dialogueText.gameObject.SetActive(false);
        }
        //Debug.Log("Finished displaying all dialogues");
        if (info.IsRepeating)
        {
            alreadyActivated = false;
        }
    }
}