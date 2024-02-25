using UnityEngine;

public class RockController : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += () => Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Rock took " + damage + " damage");
        health.TakeDamage(damage);
    }
}
