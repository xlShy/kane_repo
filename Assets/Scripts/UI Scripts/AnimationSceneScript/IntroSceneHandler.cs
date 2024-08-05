using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class IntroSceneHandler : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private SceneLoader sceneLoader;

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
        StartCoroutine(sceneFader.FadeOutTimer());
        //StartCoroutine(Transition2Stage1());
    }
    IEnumerator Transition2Stage1()
    {
        yield return new WaitForSeconds(0.8f);
        //add timer during the video
    }
}
