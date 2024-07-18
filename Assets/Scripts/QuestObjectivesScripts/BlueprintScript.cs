using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintScript : InteractableObject
{
    [SerializeField] private GameObject blueprintUI;
    [SerializeField] private List<AudioClip> openDocumentSound;
    [SerializeField] private AudioSource audioSource;
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            blueprintUI.SetActive(true);
            PlayRandomSound();
        }
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
