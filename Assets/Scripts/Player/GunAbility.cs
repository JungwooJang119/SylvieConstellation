using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be attached to the Player Component for expected behaviour
/// Utilizes StarBullets by defaults
/// </summary>
public class GunAbility : MonoBehaviour
{
    [Header("Modfications")]
    [SerializeField] private float m_cooldown;

    [Header("Temp UI Shiz")]
    [SerializeField] private Transform m_shootDirectorUI;
    [SerializeField] private Image m_cooldownUI;
    [SerializeField] private float directorRadius;
    #region Technical
    private float m_cooldownTime = 0;
    #endregion
    /// <summary>
    /// Spawns a starbullet on GameObjects current position and sends it to the specified direction
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector3 direction)
    {
        if (m_cooldownTime > Time.time) return;
        // Assuming we define the speed of the StarBullets in the projectile itself
        Pooler.Instance.SpawnProjectile(PoolerType.StarBullet, transform.position, direction);
        m_cooldownTime = Time.time + m_cooldown;
    }

    // Temporary Legacy Input
    private void Update()
    {
        // Temp UI Updater
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        m_shootDirectorUI.localPosition = Vector3.ClampMagnitude(aimDirection * directorRadius, directorRadius);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        m_shootDirectorUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_cooldownUI.fillAmount = Mathf.Clamp((m_cooldownTime - Time.time) / m_cooldown, 0, 1);
        // Legacy Input For Debugging
        if (Input.GetMouseButtonDown(1))
        {
            Shoot(aimDirection);
        }
    }
}
