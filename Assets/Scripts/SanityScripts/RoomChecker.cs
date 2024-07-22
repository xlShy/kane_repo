using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SerializableKeyValuePair<TKey, TValue>
{
    public TKey Room;
    public TValue SanityDecreaseRate;

    public SerializableKeyValuePair(TKey key, TValue value)
    {
        Room = key;
        SanityDecreaseRate = value;
    }
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<SerializableKeyValuePair<TKey, TValue>> Items = new List<SerializableKeyValuePair<TKey, TValue>>();

    public Dictionary<TKey, TValue> ToDictionary()
    {
        return Items.ToDictionary(pair => pair.Room, pair => pair.SanityDecreaseRate);
    }
}

public class RoomChecker : MonoBehaviour
{
    private static RoomChecker _instance;
    public static RoomChecker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RoomChecker>();
            }
            return _instance;
        }
    }

    [SerializeField] private SerializableDictionary<GameObject, float> roomSanityRates = new SerializableDictionary<GameObject, float>();
    private GameObject currentRoom;
    private float sanityRate;

    public static event Action<float> OnRoomChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
    public void SetPlayerLocation(GameObject room)
    {
        currentRoom = room;
    }
    public void CheckPlayerLocation(GameObject room)
    {
        //print(room.name);
        if(room == currentRoom)
        {
            return;
        }
        SetPlayerLocation(room);

        var dictionary = roomSanityRates.ToDictionary();
        if (dictionary.TryGetValue(room, out float value))
        {
            sanityRate = value;
        }

        OnRoomChanged?.Invoke(sanityRate);
    }
}