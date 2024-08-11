using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAmbienceAudio : MonoBehaviour
{
    [SerializeField] private bool isOutsideAmbienceOn;
    [SerializeField] private bool isInsideAmbienceOn;
    [SerializeField] private List<AudioSource> outsideAudio = new List<AudioSource>();
    [SerializeField] private List<AudioSource> insideAudio = new List<AudioSource>();
    [SerializeField] private bool hasReachedTargetVolume;

    public void SetOutsideAmbience(bool isSetOn)
    {
        foreach (AudioSource source in outsideAudio)
        {           
            isOutsideAmbienceOn = isSetOn;
             if (isSetOn)
                StartCoroutine(Transition2ToggleAudio(1f, source));
            else
                StartCoroutine(Transition2ToggleAudio(0f, source));

            //source.enabled = isSetOn;
        }
    }
    public void SetInsideAmbience(bool isSetOn)
    {
        foreach (AudioSource source in insideAudio)
        {            
            isInsideAmbienceOn = isSetOn;
            if (isSetOn)
                StartCoroutine(Transition2ToggleAudio(1f, source));
            else
                StartCoroutine(Transition2ToggleAudio(0f, source));

            //source.enabled = isSetOn;
        }
    }
    private IEnumerator Transition2ToggleAudio(float targetVolume, AudioSource source)
    {
        float transitionDuration = 1f;
        float elapsedTime = 0f;
        float startVolume = source.volume;

        while (elapsedTime < transitionDuration)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        source.volume = targetVolume;
    }
}
