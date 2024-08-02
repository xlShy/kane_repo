using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioHandler : MonoBehaviour
{
    [SerializeField] private bool isPlayOnce = false;
    [SerializeField] private bool isAudioPlayed = false;
    public void PlayAudioDialogue(AudioSource dialogueAudio)
    {
        if(isPlayOnce && isAudioPlayed)
        {
            return;
        }
        isAudioPlayed = true;
        dialogueAudio.Play();
    }
}
