using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsSceneFader : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string scene2Load;

    private AsyncOperation asyncLoad;

    private void OnEnable()
    {
        videoPlayer.loopPointReached += Return2Menu;
    }
    private void OnDisable()
    {
        videoPlayer.loopPointReached -= Return2Menu;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(PlayEndCredits());
    }

    IEnumerator PlayEndCredits()
    {
        yield return new WaitForSeconds(3f);

        videoPlayer.Play();
    }

    private void Return2Menu(VideoPlayer vp)
    {
        StartCoroutine(LoadMenu());
    }
    private IEnumerator LoadMenu()
    {
        asyncLoad = SceneManager.LoadSceneAsync(scene2Load);
        asyncLoad.allowSceneActivation = false; 

        yield return new WaitUntil(() => !videoPlayer.isPlaying && asyncLoad.progress >= 0.9f);
        yield return new WaitForSeconds(1f);
        asyncLoad.allowSceneActivation = true;
    }
}
