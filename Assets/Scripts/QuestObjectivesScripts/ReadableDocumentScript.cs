using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ReadableDocumentScript : InteractableObject
{
    [SerializeField] private GameObject readableDocumentUI;

    [SerializeField] private InterfaceOnClose interfaceOnCloseScript;

    [SerializeField] private List<AudioClip> openDocumentSound;
    [SerializeField] private AudioSource audioSource;

    public UnityEvent initiateOfficeEvent;

    public bool isOfficeEventStarted = false;
    private void Start()
    {
        interfaceOnCloseScript.interfaceClosed.AddListener(initiateFlickerEvent);
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            readableDocumentUI.SetActive(true);
            PlayRandomSound();
        }
    }

    private void initiateFlickerEvent()
    {
        if (!isOfficeEventStarted)
        {
            //enables light flicker
            //disables sanity
            //locks office door
            //sets bookshelf to be moved
            initiateOfficeEvent.Invoke();

            isOfficeEventStarted = true;
        }
    }

    private void PlayRandomSound()
    {
        if (openDocumentSound != null && openDocumentSound.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, openDocumentSound.Count);
            AudioClip randomClip = openDocumentSound[randomIndex];

            if (randomClip != null)
            {
                audioSource.PlayOneShot(randomClip);
            }
            else
            {
                Debug.Log("Audio clip is null");
            }
        }
        else
        {
            Debug.Log("No sound playing.");
        }
    }
}
