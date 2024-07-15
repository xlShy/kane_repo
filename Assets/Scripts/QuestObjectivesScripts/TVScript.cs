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
        fuseBoxScript.fuseBoxActivate.AddListener(initiateTelevision);
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && fuseBoxScript.isCompleted)
        {
            tvInterface.SetActive(true);
        }
    }

    private void initiateTelevision()
    {
        staticTVObject.SetActive(true);
        tvStaticLoopSound.Play();
        meshRenderer.enabled = !meshRenderer.enabled;
    }
    void ObjectiveOutline()
    {
        //this will be dedicated to outline of the objective until 1st time interact
    }
}
