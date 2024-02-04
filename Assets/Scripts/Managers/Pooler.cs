using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooler : Singleton<Pooler>
{
    [SerializeField] private PoolerObject[] m_poolerObjects;
    private Dictionary<PoolerType, Projectile2D[]> m_pooledObjects;
    private Dictionary<PoolerType, int> m_pooledIdx;
    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        if (m_poolerObjects == null)
        {
            Debug.LogWarning("Pooler requires pooler objects to be defined before playtime.");
            return;
        }
        m_pooledObjects = new Dictionary<PoolerType, Projectile2D[]>();
        m_pooledIdx = new Dictionary<PoolerType, int>();

        // Pooling the Pooler with defined Projectiles
        for (int i = 0; i < m_poolerObjects.Length; i++)
        {
            if (m_pooledObjects.ContainsKey(m_poolerObjects[i].PoolerType))
            {
                Debug.LogWarning("Pooler can only associate one prefab for every unique pooler type enum.");
                return;
            }
            m_pooledObjects.Add(m_poolerObjects[i].PoolerType, new Projectile2D[m_poolerObjects[i].SpawnCount]);
            m_pooledIdx.Add(m_poolerObjects[i].PoolerType, 0);
            for (int j = 0; j < m_poolerObjects[i].SpawnCount; j++)
            {
                if (m_poolerObjects[i].ProjectilePrefab == null)
                {
                    Debug.LogError("Pooler Cannot Initialize Null Prefabs");
                    return;
                }
                GameObject go = Instantiate(m_poolerObjects[i].ProjectilePrefab);
                go.transform.SetParent(transform, false);
                m_pooledObjects[m_poolerObjects[i].PoolerType][j] = go.GetComponent<Projectile2D>();
            }
        }
    }
    /// <summary>
    /// Indexes active or non-active projectiles of a given type and spawns it 
    /// at the given position, moving the projectile with the given direction and speed
    /// Note: For now the velocity applied is constant
    /// </summary>
    /// <param name="type"></param>
    /// <param name="spawnPosition"></param>
    /// <param name="velocity"></param>
    public void SpawnProjectile(PoolerType type, Vector2 spawnPosition, Vector2 direction, float speed)
    {
        // Error Checking
        if (spawnPosition == null || direction == null)
        {
            Debug.LogError("Cannot pass null values for spawnPosition or direction");
            return;
        }
        if (!m_pooledObjects.ContainsKey(type))
        {
            Debug.LogWarning("Pooler does not hold projectile type " + type.ToString());
            return;
        }

        // Logic
        m_pooledObjects[type][m_pooledIdx[type]].Spawn(spawnPosition, direction, speed);
        m_pooledIdx[type] = (m_pooledIdx[type] + 1) % m_pooledObjects[type].Length;
    }
    /// <summary>
    /// Indexes active or non-active projectiles of a given type and spawns it 
    /// at the given position, moving with the given direction and projectile's defined speed
    /// Note: For now the velocity applied is constant
    /// </summary>
    /// <param name="type"></param>
    /// <param name="spawnPosition"></param>
    /// <param name="velocity"></param>
    public void SpawnProjectile(PoolerType type, Vector2 spawnPosition, Vector2 direction)
    {
        // Error Checking
        if (spawnPosition == null)
        {
            Debug.LogError("Cannot pass null values for spawnPosition or direction");
            return;
        }
        if (!m_pooledObjects.ContainsKey(type))
        {
            Debug.LogWarning("Pooler does not hold projectile type " + type.ToString());
            return;
        }

        // Logic
        m_pooledObjects[type][m_pooledIdx[type]].Spawn(spawnPosition, direction);
        m_pooledIdx[type] = (m_pooledIdx[type] + 1) % m_pooledObjects[type].Length;
    }
}

public enum PoolerType
{
    StarBullet,
    Astroid
}

[Serializable]
public struct PoolerObject
{ 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private PoolerType poolerType;
    [SerializeField] private int spawnCount;

    public GameObject ProjectilePrefab => projectilePrefab;
    public PoolerType PoolerType => poolerType;
    public int SpawnCount => spawnCount;
}
