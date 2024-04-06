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
    [SerializeField] private ProjectileSourceObject[] m_projectiles;
    //[SerializeField] private PoolerType m_projectileType;
    #region Particle System References
    private ParticleSystem Ps;
    private List<ParticleSystem.Particle> Enter;
    #endregion
    #region Technical
    private float totalWeight;
    #endregion
    private void Awake()
    {
        Ps = GetComponent<ParticleSystem>();
        Enter = new List<ParticleSystem.Particle>();
        UpdateTotalWeight();
    }

    private void Start()
    {
        // Error Checking
        if (Pooler.Instance == null)
        {
            Debug.LogError("Projectile Source requires Pooler in scene");
        }
        else 
        {
            foreach (ProjectileSourceObject pso in m_projectiles)
            {
                if (!Pooler.Instance.HasPooledProjectile(pso.ProjectileType))
                {
                    Debug.LogError("Pooler does not contain specified projectile type: " + pso.ProjectileType);
                    break;
                }
            } 
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
        UpdateTotalWeight();
    } 
    private void OnParticleTrigger()
    {
        int numInside = Ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, Enter);
        PoolerType randomProjectile = PickRandomProjectile();
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle particle = Enter[i];

            if (!Pooler.Instance.IsParticleMaximallyPooled(randomProjectile))
            {
                Projectile2D projectile = Pooler.Instance.SpawnProjectile(randomProjectile, particle.position, particle.velocity.normalized, particle.velocity.magnitude);
                projectile.RB.angularVelocity = particle.angularVelocity;
            }   
        }
    }
    private void UpdateTotalWeight()
    {
        totalWeight = 0f;
        foreach (ProjectileSourceObject pso in m_projectiles) 
        {
            totalWeight += pso.SpawnWeight;
        }
    }
    private PoolerType PickRandomProjectile()
    {
        float randomWeight = UnityEngine.Random.Range(0f, totalWeight);
        foreach (ProjectileSourceObject pso in m_projectiles)
        {
            randomWeight -= pso.SpawnWeight;
            if (randomWeight <= 0)
            {
                return pso.ProjectileType;
            }
        }

        return m_projectiles[0].ProjectileType;
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
