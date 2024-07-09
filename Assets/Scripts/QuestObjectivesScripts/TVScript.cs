using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVScript : InteractableObject
{
    public GameObject tvInterface;
    private Renderer objectRenderer;
    [SerializeField] private FuseBox fuseBoxScript;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2 && fuseBoxScript.isCompleted)
        {
            tvInterface.SetActive(true);
           
        }
    }

    void ObjectiveOutline()
    {
        //this will be dedicated to outline of the objective until 1st time interact
    }
}
