using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditsSceneFader : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    private void Start()
    {
        StartCoroutine(PlayEndCredits());
    }

    IEnumerator PlayEndCredits()
    {
        yield return new WaitForSeconds(3f);

        videoPlayer.Play();
    }
}
