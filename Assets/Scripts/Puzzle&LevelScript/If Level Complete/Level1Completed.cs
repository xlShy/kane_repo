using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Completed : MonoBehaviour
{
    [SerializeField] private SanityHandler sanityHandler;
    [SerializeField] private LoadScene loadScene;
    [SerializeField] private SceneObjectsLoader sceneObjectsLoader;

    public void CompleteLevel1()
    {
        print("scene transition");
        sanityHandler.sanityValue = 0.05f;
        StartCoroutine(SetTimer2NextScene());
    }
    IEnumerator SetTimer2NextScene()
    {
        sceneObjectsLoader.Object2LoadOnScene();
        yield return new WaitForSeconds(3f);
        loadScene.LoadNextScene("Stage 2");
    }
}
