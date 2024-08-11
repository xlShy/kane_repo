using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioHandler : MonoBehaviour
{
    [SerializeField] private bool isPlayOnce = false;
    [SerializeField] private bool isAudioPlayed = false;
    public bool isPlaying;
    public AudioSource dialogueAudio;
    public void PlayAudioDialogue(AudioSource dialogueAudioSource)
    {
        dialogueAudio.clip = dialogueAudioSource.clip;
        if (dialogueAudio.isPlaying)
        {
            isPlaying = true;
            WaitUntilDialougueIsFinished();
            if (isPlaying)
            {
                return;
            }
        }
        if (isPlayOnce && isAudioPlayed)
        {
            return;
        }
        isAudioPlayed = true;
        dialogueAudio.Play();   
    }
    private IEnumerator WaitUntilDialougueIsFinished()
    {
        yield return new WaitUntil(() => !dialogueAudio.isPlaying);
        isPlaying = false;
        this.dialogueAudio = null;
    }
}
