using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStarExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particlesBottom, particlesTop;

    public void ExplodeBottom()
    {
        foreach (ParticleSystem p in particlesBottom) {
            p.Play();
            print("bullshit");
        }
    }

    public void ExplodeTop()
    {
        foreach (ParticleSystem p in particlesTop) {
            p.Play();
        }
    }
}
