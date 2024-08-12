using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleAudio : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource particleAudio;
    public bool isPlayed;
    void Update()
    {
        if (particle.isPlaying)
        {
            if (isPlayed)
            {
                return;
            }
            particleAudio.Play();
            isPlayed = true;
        }
    }
}
