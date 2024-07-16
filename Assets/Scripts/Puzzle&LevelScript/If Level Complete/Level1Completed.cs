using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Completed : MonoBehaviour
{
    [SerializeField] private SanityHandler sanityHandler;
    [SerializeField] private LoadScene loadScene;

    private void OnEnable()
    {
        LevelManager.OnCompleteLevel1 += CompleteLevel1;
    }
    private void OnDisable()
    {
        LevelManager.OnCompleteLevel1 -= CompleteLevel1;
    }
    public void CompleteLevel1()
    {
        print("scene transition");
        //sanityHandler.sanityValue = 0.05f;
        
        //loadScene.LoadNextScene("Stage 2");

    }
}
