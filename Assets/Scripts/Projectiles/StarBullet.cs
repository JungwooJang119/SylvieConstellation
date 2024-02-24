using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : Projectile2D
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") return;
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        base.OnCollisionEnter2D(collision);
    }
}
