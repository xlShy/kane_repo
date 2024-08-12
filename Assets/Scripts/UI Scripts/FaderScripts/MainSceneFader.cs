using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainSceneFader : MonoBehaviour
{
    public AudioSource startAudio;

    public UnityEvent OnGameStart;
    void Start()
    {
        StartCoroutine(StartGameWithDelay());
    }

    private IEnumerator StartGameWithDelay()
    {
        yield return StartCoroutine(StartDelay(5f));
        OnGameStart?.Invoke();
    }
    private IEnumerator StartDelay(float delay)
    {
        startAudio.Play();
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
