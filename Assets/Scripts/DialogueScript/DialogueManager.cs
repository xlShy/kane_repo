using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    [SerializeField] private Text dialogueText;
    private Coroutine dialogueCoroutine;
    public bool isDisplayingDialogue = false;
    private bool isInteractDialogue = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool StartDialogue(List<string> content, float duration, bool isInteract = false)
    {
        if (!isDisplayingDialogue || (isInteract && !isInteractDialogue))
        {
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
            }
            dialogueCoroutine = StartCoroutine(DisplayDialogue(content, duration, isInteract));
            return true;
        }
        return false;
    }

    private IEnumerator DisplayDialogue(List<string> content, float duration, bool isInteract)
    {
        isDisplayingDialogue = true;
        isInteractDialogue = isInteract;

        for (int i = 0; i < content.Count; i++)
        {
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = content[i];
            yield return new WaitForSeconds(duration);
            dialogueText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        isDisplayingDialogue = false;
        isInteractDialogue = false;
        dialogueCoroutine = null;
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