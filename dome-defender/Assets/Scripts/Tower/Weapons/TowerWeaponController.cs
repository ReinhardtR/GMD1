using UnityEngine;

public class TowerWeaponController : MonoBehaviour
{
    public GameObject Projectile;
    public Transform FirePoint;

    private readonly float projectileSpeed = 5f;
    private readonly float fireRate = 0.3f;
    private float nextFire = 0f;

    public void FireWeapon()
    {
        if (Time.time < nextFire)
        {
            return;
        }

        nextFire = Time.time + fireRate;

        GameObject projectile = Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
    }
}
