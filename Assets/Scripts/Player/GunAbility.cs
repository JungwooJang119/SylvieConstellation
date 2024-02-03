using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Must be attached to the Player Component for expected behaviour
/// Utilizes StarBullets by defaults
/// </summary>
public class GunAbility : MonoBehaviour
{
    /// <summary>
    /// Spawns a starbullet on GameObjects current position and sends it to the specified direction
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector3 direction)
    {
        // Assuming we define the speed of the StarBullets in the projectile itself
        Pooler.Instance.SpawnProjectile(PoolerType.StarBullet, transform.position, direction);
    }

    // Temporary Legacy Input
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot(mousePosition - transform.position);
        }
    }
}
