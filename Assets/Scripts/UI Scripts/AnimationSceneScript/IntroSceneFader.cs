using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroSceneFader : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private string scene2Load;
    private AsyncOperation asyncLoad;

    public UnityEvent OnStartVideo;

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
    private void Start()
    {
        OnStartVideo?.Invoke();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        print("Start Transition to level 1");
        StartCoroutine(sceneFader.FadeOutTimer());
        StartCoroutine(Transition2Stage1());
    }
    IEnumerator Transition2Stage1()
    {
        yield return new WaitForSeconds(0.8f);

        asyncLoad = SceneManager.LoadSceneAsync(scene2Load);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitUntil(() => !videoPlayer.isPlaying && asyncLoad.progress >= 0.9f);

        asyncLoad.allowSceneActivation = true;
    }
}
