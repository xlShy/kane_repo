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

    public void CompleteLevel1()
    {
        print("scene transition");
        sanityHandler.sanityValue = 0.1f;
        StartCoroutine(SetTimer2NextScene());
    }
    IEnumerator SetTimer2NextScene()
    {
        sceneObjectsLoader.Object2LoadOnScene();
        yield return new WaitForSeconds(5f);
        loadScene.LoadNextScene("Stage 2");
    }
}
