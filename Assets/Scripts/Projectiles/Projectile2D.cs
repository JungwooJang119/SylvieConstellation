using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile2D : MonoBehaviour
{
    [SerializeField] protected Vector2 m_constantVelocity;
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
    public void Spawn(Vector2 position, Vector2 velocity)
    {
        gameObject.SetActive(true);
        transform.position = position;
        m_rb.velocity = velocity;
    }
    public void Spawn(Vector2 position)
    {
        Spawn(position, m_constantVelocity);
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
