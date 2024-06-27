using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onHover : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ClickAudioClip;
    public AudioClip HoverAudioClip;

    public void OnPointerHover()
    {
        audioSource.PlayOneShot(HoverAudioClip);
    }

    public void OnPointerClick()
    {
        audioSource.PlayOneShot(ClickAudioClip);
    }


}
