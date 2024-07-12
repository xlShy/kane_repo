using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectsLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] ddolObjects;
    public string sceneName;

    public LoadScene scene;
    public void Object2LoadOnScene()
    {
        foreach(GameObject obj in ddolObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(sceneName);
            Object2LoadOnScene();
        }
    }

}
