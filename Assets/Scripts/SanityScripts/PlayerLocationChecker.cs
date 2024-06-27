using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationChecker : MonoBehaviour
{
    //To Do - move script to the game manager object

    public RoomChecker roomChecker;

    RaycastHit hit;
    int hitLayer;
    GameObject hitRoom;

    [SerializeField] private float raycastDistance = 5f;

    public LayerMask mask;
    [SerializeField] private LayerMask houseMask;
    [SerializeField] private LayerMask shedMask;

    public static event Action OnEnterHouse;
    public static event Action OnEnterShed;
    public static event Action OnExitOutside;

    public bool hasEnteredSomething = false;

    [SerializeField] private float checkInterval = 3f;

    public void Start()
    {
        InvokeRepeating(nameof(CheckPlayerOnEntryExit), 0f, checkInterval);
    }
    private void Update()
    {
        if (houseMask == (houseMask | (1 << hitLayer)))
        {
            Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.green);
        }
        else if (shedMask == (shedMask | (1 << hitLayer)))
        {
            Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
        }
    }
    public  void CheckPlayerOnEntryExit()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, mask))
        {
            hitLayer = hit.collider.gameObject.layer;
            hitRoom = hit.collider.gameObject;
            if (houseMask == (houseMask | (1 << hitLayer)))
            {
                roomChecker.CheckPlayerLocation(hitRoom);
                if (hasEnteredSomething)
                {
                    return;
                }
                OnPlayerEnterHouse();
                hasEnteredSomething = true;               
            }
            else if (shedMask == (shedMask | (1 << hitLayer)))
            {
                if (hasEnteredSomething)
                {
                    return;
                }
                OnPlayerEnterShed();
                hasEnteredSomething = true;
            }
        }
        else
        {            
            if (!hasEnteredSomething)
            {
                return;
            }
            OnPlayerExitOutside();
            hasEnteredSomething = false;
        }
    }
    private void OnPlayerEnterHouse()
    {
        OnEnterHouse?.Invoke();
    }
    private void OnPlayerEnterShed()
    {
        OnEnterShed?.Invoke();
    }
    private void OnPlayerExitOutside()
    {
        OnExitOutside?.Invoke();
    }
}
