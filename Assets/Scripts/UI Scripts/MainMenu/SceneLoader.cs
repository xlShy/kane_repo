using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadVideo(sceneIndex));
    }
    IEnumerator LoadVideo(int sceneIndex)
    {
        StartCoroutine(sceneFader.FadeOutTimer());
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneIndex);
    }
}
