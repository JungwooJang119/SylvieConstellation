using System.Security.Cryptography;
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

    #region Projectile States
    public bool IsDestroyed => isDestroyed;
    private bool isDestroyed;
    #endregion

    #region References
    private Rigidbody2D m_rb;
    public Rigidbody2D RB => m_rb;
    #endregion

    #region Technical
    private int startingHP;
    #endregion
    protected void Awake()
    {
        startingHP = m_hp;
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
    /// <summary>
    /// Sets the projectile's global position to the spawnPosition and then moves a direction given a speed
    /// Direction and speed is seperated so that if a call to this wants a different speed from the default, it will be made.
    /// Spawn with a specified hp
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    /// <param name="hp"></param>
    public virtual void Spawn(Vector2 spawnPosition, Vector2 direction, float speed, int hp)
    {
        isDestroyed = false;
        m_hp = hp;
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        m_rb.velocity = direction * speed;
    }
    /// <summary>
    /// Spawn with hp the projectile started with
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public void Spawn(Vector2 spawnPosition, Vector2 direction, float speed)
    {
        Spawn(spawnPosition, direction, speed, startingHP);
    }
    /// <summary>
    /// Use's Projectile Default Speed and starting hp
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="direction"></param>
    public void Spawn(Vector2 spawnPosition, Vector2 direction)
    {
        Spawn(spawnPosition, direction, m_defaultSpeed, startingHP);
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
