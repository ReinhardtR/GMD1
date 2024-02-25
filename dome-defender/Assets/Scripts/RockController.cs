using UnityEngine;

public class RockController : MonoBehaviour
{
    public Health health;

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += () => Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Drill"))
        {
            Debug.Log("Drill hit rock");
            health.TakeDamage(100);
        }
    }
}
