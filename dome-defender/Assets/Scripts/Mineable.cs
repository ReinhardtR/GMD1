using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Mineable : MonoBehaviour
{
    public RockType RockType { get; set; }
    public int DropAmount { get; set; } = 1;

    private Health health;
    public event Action OnMineEvent;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        health.OnDeathEvent += OnDeath;
        if (RockType != null) health.MaxHealth = RockType.MaxHealth;
    }

    void OnDisable()
    {
        if (health) health.OnDeathEvent -= OnDeath;
    }

    public void TakeDamage(int damage)
    {
        // Debug.Log($"Mineable took {damage} damage");
        health.TakeDamage(damage);
        OnMineEvent?.Invoke();
    }

    private void OnDeath()
    {
        if (RockType.DropItem != null)
        {
            ItemSpawner.SpawnItem(RockType.DropItem, transform.position, DropAmount);
        }

        Destroy(gameObject);
    }
}
