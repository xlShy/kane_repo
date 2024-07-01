using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScript : InteractableObject
{

    public GameObject tvInterface;
    private Renderer objectRenderer;
    [SerializeField]
    public LayerMask newLayerMask;


    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public override void Interact(int itemInteractedCase, Inventory inventory)
    {
        if (itemInteractedCase == 2)
        {
            tvInterface.SetActive(true);
        }
    }

    void ObjectiveOutline()
    {
        //this will be dedicated to outline of the objective until 1st time interact
    }
}
