using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedRespawnPoint : MonoBehaviour
{
    public GameObject ShedSpawnPoint;
    private static ShedRespawnPoint _instance;
    public static ShedRespawnPoint Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
