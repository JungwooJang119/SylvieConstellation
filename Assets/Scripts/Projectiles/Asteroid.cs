using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Projectile2D, IDamageable
{
    #region References
    private Animator m_animator;
    #endregion

    #region Technical
    private int delta;
    private const string FULL = "Asteroid_FullHP";
    private const string MID = "Asteroid_MidHP";
    private const string LOW = "Asteroid_LowHP";
    private const string DEAD = "Asteroid_DeadHP";
    #endregion

    private new void Awake()
    {
        base.Awake();
        delta = (int)Mathf.Ceil(m_hp / 3f); // Assuming that m_hp starts with full hp
        m_animator = GetComponent<Animator>();
    }

    public override void Spawn(Vector2 spawnPosition, Vector2 direction, float speed, int hp)
    {
        m_animator?.Play(FULL);
        base.Spawn(spawnPosition, direction, speed, hp);
    }

    public void Damage(int amount)
    {
        m_hp -= amount;

        if (m_hp <= 0)
        {
            m_animator?.Play(DEAD);
            // Logic for asteriod destruction state
            // Could just remove collider instead of setting active so that we have a destroyed state uwu
            gameObject.SetActive(false);
            return;
        }

        m_animator?.Play(m_hp <= delta ? LOW : (m_hp <= (2 * delta) ? MID : FULL));
    }


}
