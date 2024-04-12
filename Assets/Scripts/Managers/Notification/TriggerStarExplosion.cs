using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStarExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    public void Explode()
    {
        foreach (ParticleSystem p in particles) {
            p.Play();
        }
    }
}
