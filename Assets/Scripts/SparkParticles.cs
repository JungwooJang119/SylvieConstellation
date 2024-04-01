using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkParticles : MonoBehaviour
{
    public ParticleSystem sparksParticleSystem;

    private void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.CompareTag("Obstacle")) {
            var emission = sparksParticleSystem.emission;
            emission.enabled = true;
            sparksParticleSystem.Play();
        }

    }
}
