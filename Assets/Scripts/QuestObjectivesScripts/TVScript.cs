using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TVScript : InteractableObject
{
    public GameObject tvInterface;
    [SerializeField] public GameObject staticTVObject;
    [SerializeField] private FuseBox fuseBoxScript;
    [SerializeField] private AudioSource tvStaticLoopSound;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && fuseBoxScript.isCompleted)
        {
            tvInterface.SetActive(true);
        }
    }
    public void enableTelevision()
    {
        staticTVObject.SetActive(true);
        tvStaticLoopSound.Play();
        meshRenderer.enabled = !meshRenderer.enabled;
    }
}
