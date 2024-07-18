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

    public void CompleteLevel1()
    {
        print("scene transition");
        //create function that will make a blackout instead of using sanity value
        //sanityHandler.sanityValue = 0.055f;
        StartCoroutine(SetTimer2NextScene());
    }
    IEnumerator SetTimer2NextScene()
    {
        sceneObjectsLoader.Object2LoadOnScene();
        yield return new WaitForSeconds(5f);
        loadScene.LoadNextScene("Stage 2");
        //player.transform.position = playerData.playerPositionOnSpawn.transform.position;
        
    }
}
