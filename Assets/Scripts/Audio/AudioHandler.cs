using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioClip[] sanityAudioClips;

    public AudioSource audioSource;

    public void PlaySelectedAudio(int index)
    {
        if (index >= 0 && index < sanityAudioClips.Length)
        {
            audioSource.Stop(); 
            audioSource.clip = sanityAudioClips[index];
            audioSource.loop = true; 
            audioSource.Play();
        }
    }
    public void StopSanityAudio()
    {
        audioSource.GetComponent<AudioSource>().Stop();
    }
}
