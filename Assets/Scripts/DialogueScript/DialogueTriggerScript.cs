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
    private Coroutine dialogueCoroutine;
    private int currentDialogueIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyActivated && dialogueContent.Count > 0)
        {
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
            }
            dialogueCoroutine = StartCoroutine(DisplayAllDialogues());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isRepeating)
        {
            alreadyActivated = true;
        }
    }

    private IEnumerator DisplayAllDialogues()
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
