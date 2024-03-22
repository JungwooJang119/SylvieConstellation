using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Projectile2D : MonoBehaviour
{
    [Header("Projectile Details")]
    [SerializeField] protected int m_dmg;
    [SerializeField] protected int m_hp = 1;
    [SerializeField,Tooltip("Projectile's Default Speed")] 
    protected float m_defaultSpeed;
    [SerializeField] protected bool m_destroyOnHit;

    [Header("Projectile Dev")]
    [SerializeField] protected bool m_spawnOnStart;
    [SerializeField] protected Vector2 m_velocityDirection;
    
    // Projectile States
    public bool IsDestroyed => isDestroyed;
    private bool isDestroyed;

    // Attached Components 
    private Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

        if (m_spawnOnStart)
        {
            isDestroyed = false;
            m_rb.velocity = m_velocityDirection * m_defaultSpeed;
            return;
        }
        isDestroyed = true;
        gameObject.SetActive(false);
    }
    public void Spawn(Vector2 spawnPosition, Vector2 direction, float speed)
    {
        isDestroyed = false;
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        m_rb.velocity = direction * speed;
    }
    /// <summary>
    /// Use's Projectile Default Speed
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="direction"></param>
    public void Spawn(Vector2 spawnPosition, Vector2 direction)
    {
        Spawn(spawnPosition, direction, m_defaultSpeed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) 
        {
            damageable.Damage(m_dmg);
        }
        if (m_destroyOnHit)
        {
            isDestroyed = true;
            gameObject.SetActive(false);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(m_dmg);
        }
        if (m_destroyOnHit)
        {
            isDestroyed = true;
            gameObject.SetActive(false);
        }
    }
}
