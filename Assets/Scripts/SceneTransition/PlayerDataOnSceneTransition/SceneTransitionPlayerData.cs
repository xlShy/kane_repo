using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneTransitionPlayerData", menuName = "Scene/Transition Data")]
public class SceneTransitionPlayerData : ScriptableObject
{
    public GameObject playerPositionOnSpawn;
    public float sanityValueOnSpawn;
}
