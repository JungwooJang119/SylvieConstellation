using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : Projectile2D
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.GetComponent<StarBullet>()) return;
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<StarBullet>()) return;
        base.OnCollisionEnter2D(collision);
    }
}
