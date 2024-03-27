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
    // TODO:
    //[SerializeField] private Vector3 m_gunSourceLocalPosition; // Make it either an offset or something else
    [SerializeField] private float m_cooldown;
    [SerializeField] private float m_accuracyChargeSpeed = 2f;
    [SerializeField] private float m_recoilMagnitude = 20f;
    
    [Header("Temp UI Shiz")]
    [SerializeField] private Transform m_shootDirectorUI;
    [SerializeField] private Image m_cooldownUI;
    [SerializeField] private float directorRadius;
    [SerializeField] private Transform m_accuracyLineUpUI;
    [SerializeField] private Transform m_accuracyLineDownUI;
    #region GameObject Component References
    private Rigidbody2D m_rb;
    #endregion
    #region Technical
    private float m_cooldownTime = 0;
    private float m_angleCharge;
    private bool charged;
    #endregion
    private void Start()
    {
        // Component Reference Initialization
        m_rb = GetComponent<Rigidbody2D>();

        // Initially Not Shown
        m_accuracyLineUpUI.gameObject.SetActive(false);
        m_accuracyLineDownUI.gameObject.SetActive(false);
    }
    /// <summary>
    /// Spawns a starbullet on GameObjects current position and sends it to the specified direction
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector2 direction, float angleOffset)
    {
        if (!charged) return;
        // Assuming we define the speed of the StarBullets in the projectile itself
        Pooler.Instance.SpawnProjectile(PoolerType.StarBullet, transform.position, direction);
        // Two other projectiles firing at a certain angle
        Vector3 angledUpDirection = Quaternion.AngleAxis(angleOffset, Vector3.forward) * direction;
        Pooler.Instance.SpawnProjectile(PoolerType.StarBullet, transform.position, angledUpDirection);

        Vector3 angledDownDirection = Quaternion.AngleAxis(-1 * angleOffset, Vector3.forward) * direction;
        Pooler.Instance.SpawnProjectile(PoolerType.StarBullet, transform.position, angledDownDirection);
        m_cooldownTime = Time.time + m_cooldown;

        // Recoil the model by a modifed force
        m_rb.AddForce(-1 * m_recoilMagnitude * direction, ForceMode2D.Impulse);
        charged = false;
    }

    public void ChargeUp()
    {
        if (!charged) return;
        m_angleCharge -= Time.fixedDeltaTime * m_accuracyChargeSpeed;
        m_angleCharge = Mathf.Clamp(m_angleCharge, 0f, 45f);
    }
    public void PrepareGun()
    {
        if (m_cooldownTime > Time.time) return;
        charged = true;
        m_angleCharge = 30f;
    }

    // Temporary Legacy Input
    private void Update()
    {
        // Temp UI Updater
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Director UI
        m_shootDirectorUI.localPosition = Vector3.ClampMagnitude(aimDirection * directorRadius, directorRadius);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        m_shootDirectorUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_cooldownUI.fillAmount = Mathf.Clamp((m_cooldownTime - Time.time) / m_cooldown, 0, 1);

        // Accuracy
        float angleChargeOffset = m_angleCharge + 5f;
        m_accuracyLineUpUI.localRotation = Quaternion.AngleAxis(angleChargeOffset, Vector3.forward);
        m_accuracyLineDownUI.localRotation = Quaternion.AngleAxis(-1 * angleChargeOffset, Vector3.forward);

        // TODO:
        // Problems with this implementation:
        // The cooldown time is dependent only on what is fired rather than what should be fired in order thus
        // COOLDOWN time needs to make it not possible to charge this up at all and require charging before you can fire!!
        //
        // (Ryan) I believe this has been done

        // Legacy Input For Debugging
        // Initiate Gun
        if (Time.time >= m_cooldownTime && Input.GetMouseButtonDown(1))
        {
            PrepareGun();
            ChargeUp();
            m_accuracyLineUpUI.gameObject.SetActive(true);
            m_accuracyLineDownUI.gameObject.SetActive(true);
        }
        // Charge gun accuracy
        if (Input.GetMouseButton(1))
        {
            ChargeUp();  
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_accuracyLineUpUI.gameObject.SetActive(false);
            m_accuracyLineDownUI.gameObject.SetActive(false);
            Shoot(aimDirection, m_angleCharge);
        }
    }
}
