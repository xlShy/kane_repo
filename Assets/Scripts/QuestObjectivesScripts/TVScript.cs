using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TVScript : InteractableObject
{
    public GameObject tvInterface;
    private Renderer objectRenderer;
    [SerializeField] private FuseBox fuseBoxScript;
    [SerializeField] private AudioSource tvStaticLoopSound;

    private void Start()
    {
        fuseBoxScript.fuseBoxActivate.AddListener(initiateTelevision);
        objectRenderer = GetComponent<Renderer>();
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
        tvStaticLoopSound.Play();
    }
    void ObjectiveOutline()
    {
        //this will be dedicated to outline of the objective until 1st time interact
    }
}
