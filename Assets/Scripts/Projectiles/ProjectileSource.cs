using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this on a particle system with a trigger module enabled
/// You need to have a collider to get the projectiles to trigger 
/// All setting of velocity and times are all dependent on the particle system settings
/// </summary>
[RequireComponent(typeof(ParticleSystem), typeof(Collider2D))]
public class ProjectileSource : MonoBehaviour
{
    [Header("Source Specfications")]
    //[SerializeField] private ProjectileSourceObject[] m_projectiles;
    [SerializeField] private PoolerType m_projectileType;
    #region Particle System References
    private ParticleSystem Ps;
    private List<ParticleSystem.Particle> Enter;
    #endregion
    #region Technical
    private float total;
    #endregion
    private void Awake()
    {
        Ps = GetComponent<ParticleSystem>();
        Enter = new List<ParticleSystem.Particle>();
    }

    private void Start()
    {
        // Error Checking
        if (Pooler.Instance == null)
        {
            Debug.LogError("Projectile Source requires Pooler in scene");
        }
        else if (!Pooler.Instance.HasPooledProjectile(m_projectileType))
        {
            Debug.LogError("Pooler does not contain specified projectile type: " + m_projectileType);
        }
    }

    private void OnValidate()
    {
        Ps = GetComponent<ParticleSystem>();
        var trigger = Ps.trigger;
        if (trigger.enter != ParticleSystemOverlapAction.Callback)
        {
            trigger.enter = ParticleSystemOverlapAction.Callback;
        }
    } 

    private void OnParticleTrigger()
    {
        int numInside = Ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, Enter);
        Debug.Log("Triggered: " + numInside);
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle particle = Enter[i];

            if (!Pooler.Instance.IsParticleMaximallyPooled(m_projectileType))
            {
                Pooler.Instance.SpawnProjectile(m_projectileType, particle.position + transform.position, particle.velocity.normalized, particle.velocity.magnitude);
            }   
        }
    }
}


// For Variable Projectile Spawning
[Serializable]
public struct ProjectileSourceObject
{
    [SerializeField] private PoolerType projectileType;
    [SerializeField] private float spawnWeight;

    public PoolerType ProjectileType => projectileType;
    public float SpawnWeight => spawnWeight;
}
