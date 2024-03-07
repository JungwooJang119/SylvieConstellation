using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : Projectile2D, IDamageable
{
    public void Damage(int amount)
    {
        m_hp -= amount;
        if (m_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
