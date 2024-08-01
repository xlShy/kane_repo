using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{
    public void EnableNonLoopingParticle(ParticleSystem particle)
    {
        particle.Play();
    }
}
