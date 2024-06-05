using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int Damage = 10;
    public float Speed = 10f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AudioManager.Instance.Play("WeaponFire");
        rb.velocity = transform.up * Speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }

    }

    void OnBecameInvisible()
    {
        // give the projectile a little time
        // in case the player went out of the tower control center
        // assuming the projectile would hit the enemy eventually
        Destroy(gameObject, 2f);
    }
}
