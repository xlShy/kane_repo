using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] private Text dialogueText;
    private Queue<DialogueInfo> dialogueQueue = new Queue<DialogueInfo>();
    private Coroutine dialogueCoroutine;
    private bool isDisplayingDialogue = false;
    private const int MAX_QUEUE_SIZE = 2;

    private class DialogueInfo
    {
        public List<string> Content;
        public float Duration;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogueCoroutine = StartCoroutine(ProcessDialogueQueue());
    }

    public bool EnqueueDialogue(List<string> content, float duration)
    {
        if (dialogueQueue.Count < MAX_QUEUE_SIZE)
        {
            DialogueInfo info = new DialogueInfo
            {
                Content = new List<string>(content),
                Duration = duration
            };
            dialogueQueue.Enqueue(info);
            return true;
        }
        return false;
    }

    private IEnumerator ProcessDialogueQueue()
    {
        while (true)
        {
            if(dialogueQueue.Count > 0 && !isDisplayingDialogue)
            {
                DialogueInfo currentDialogue = dialogueQueue.Dequeue();
                yield return DisplayAllDialogues(currentDialogue);
            }
            yield return null;
        }
    }

    private IEnumerator DisplayAllDialogues(DialogueInfo info)
    {
        isDisplayingDialogue = true;
        for (int i = 0; i < info.Content.Count; i++)
        {
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = info.Content[i];
            yield return new WaitForSeconds(info.Duration);
            dialogueText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        isDisplayingDialogue = false; 
    }

    private void OnDisable()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }
    }
}
