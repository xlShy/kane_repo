using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Completed : MonoBehaviour
{
    [SerializeField] private SanityHandler sanityHandler;
    [SerializeField] private LoadScene loadScene;
    [SerializeField] private SceneObjectsLoader sceneObjectsLoader;
    [SerializeField] private SanityStatusEffect sanityStatusEffect;
    [SerializeField] private GameObject player;
    [SerializeField] private SceneTransitionPlayerData playerData;

    private bool isComplete = false;
    private bool isNextScene = false;

    private void Update()
    {
        Go2NextScene();
    }
    public void CompleteLevel1()
    {
        if (isComplete)
        {
            return;
        }
        sanityHandler.sanityValue = 0.055f;
        isComplete = true;

    }
    private void Go2NextScene()
    {   
        if (sanityHandler.isSanityDepleted && isComplete && !isNextScene)
        {
            sanityHandler.isSanityDepleted = false;
            sceneObjectsLoader.Object2LoadOnScene();
            loadScene.LoadNextScene("Stage 2");
            isNextScene = true; 
        }
        //player.transform.position = playerData.playerPositionOnSpawn.transform.position;   
    }
}
