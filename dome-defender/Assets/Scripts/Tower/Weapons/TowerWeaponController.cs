using System.Collections.Generic;
using UnityEngine;

public class TowerWeaponController : MonoBehaviour
{
    public GameObject Projectile;
    public Transform FirePoint;

    [Header("Projectile")]
    public int Damage = 10;
    public float FireRate = 0.3f;
    public float nextFireTime = 0f;

    [Header("Melee")]
    public int MeleeDamage = 10;
    public float MeleeAttackRate = 0.5f;
    public float MeleeNextAttackTime = 0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < MeleeNextAttackTime)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            Collider2D[] enemies = Physics2D.OverlapBoxAll(FirePoint.position, new Vector2(1, 1), 0, LayerMask.GetMask("Enemy"));
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.TryGetComponent(out Health enemyHealth))
                {
                    enemyHealth.TakeDamage(MeleeDamage);
                }
            }
            MeleeNextAttackTime = Time.time + MeleeAttackRate;
        }
    }

    public void FireWeapon()
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        nextFireTime = Time.time + FireRate;

        GameObject projectile = Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
        if (projectile.TryGetComponent(out ProjectileController projectileController))
        {
            projectileController.Damage = Damage;
        }
    }
}
