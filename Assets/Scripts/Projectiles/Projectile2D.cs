using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile2D : MonoBehaviour
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected int m_dmg;
    [SerializeField] protected int m_hp = 1;
    [SerializeField] protected bool m_destroyOnHit;
    // Attached Components 
    private Rigidbody2D m_rb;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }
    public void Spawn(Vector2 spawnPosition, Vector2 direction, float speed)
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        m_rb.velocity = direction * speed;
    }
    public void Spawn(Vector2 spawnPosition, Vector2 direction)
    {
        Spawn(spawnPosition, direction * m_speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) 
        {
            damageable.Damage(m_dmg);
        }
        if (m_destroyOnHit)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(m_dmg);
        }
        if (m_destroyOnHit)
        {
            gameObject.SetActive(false);
        }
    }
}
