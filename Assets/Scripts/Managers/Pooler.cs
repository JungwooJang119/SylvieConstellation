using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooler : Singleton<Pooler>
{
    [SerializeField] private PoolerObject[] m_poolerObjects;
    private List<Projectile2D[]> m_pooledObjects;
    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        m_pooledObjects = new List<Projectile2D[]>();
        for (int i = 0; i < m_poolerObjects.Length; i++)
        {
            m_pooledObjects.Add(new Projectile2D[m_poolerObjects[i].SpawnCount]);
            for (int j = 0; j < m_poolerObjects[i].SpawnCount; j++)
            {
                m_pooledObjects[i][j] = Instatiate(m_poolerObjects[i].Projectile);
            }
        }
    }

    public void SpawnProjectile(PoolerType type)
    {
        switch (type)
        {
            case PoolerType.StarBullet:
                
                break;
        }
    }
}

public enum PoolerType
{
    StarBullet,
    Astroid
}

public struct PoolerObject
{ 
    [SerializeField] private Projectile2D projectile;
    [SerializeField] private PoolerType poolerType;
    [SerializeField] private int spawnCount;

    public Projectile2D Projectile => projectile;
    public PoolerType PoolerType => poolerType;
    public int SpawnCount => spawnCount;
}
