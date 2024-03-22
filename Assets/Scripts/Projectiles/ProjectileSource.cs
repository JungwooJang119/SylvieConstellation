using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this on a particle system with a trigger module enabled
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ProjectileSource : MonoBehaviour
{
    [Header("Source Specfications")]
    [SerializeField] private PoolerType m_projectileType;
    ParticleSystem Ps;
    List<ParticleSystem.Particle> Enter;
    void Awake()
    {
        Ps = GetComponent<ParticleSystem>();
        Enter = new List<ParticleSystem.Particle>();
    }

    void OnValidate()
    {
        Ps = GetComponent<ParticleSystem>();
        var trigger = Ps.trigger;
        if (trigger.enter != ParticleSystemOverlapAction.Callback)
        {
            trigger.enter = ParticleSystemOverlapAction.Callback;
        }
    }

    void OnParticleTrigger()
    {
        Debug.Log("Triggered");
        // get
        int numInside = Ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, Enter);

        // on enter
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle particle = Enter[i];

            if (Pooler.Instance.IsParticleMaximallyPooled(m_projectileType))
            {
                Pooler.Instance.SpawnProjectile(m_projectileType, particle.position, particle.velocity.normalized, particle.velocity.magnitude);
            }   
        }
    }
}
