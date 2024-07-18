using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ReadableDocumentScript : InteractableObject
{
    [SerializeField] private GameObject readableDocumentUI;

    [SerializeField] private InterfaceOnClose interfaceOnCloseScript;

    public UnityEvent initiateFlicker;

    [SerializeField] private List<AudioClip> openDocumentSound;
    [SerializeField] private AudioSource audioSource;
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
        initiateFlicker.Invoke();
        //sanity drain disable
    }

    private void PlayRandomSound()
    {
        if (openDocumentSound != null && openDocumentSound.Count > 0)
        {
            int randomIndex = Random.Range(0, openDocumentSound.Count);
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
