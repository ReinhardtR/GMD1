using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Mineable : MonoBehaviour
{
    public Item DropItem { get; set; }
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
    }

    void OnDisable()
    {
        if (health)
        {
            health.OnDeathEvent -= OnDeath;
        }
    }

    public void TakeDamage(int damage)
    {
        // Debug.Log($"Mineable took {damage} damage");
        health.TakeDamage(damage);
        OnMineEvent?.Invoke();
    }

    private void OnDeath()
    {
        if (DropItem != null)
        {
            ItemSpawner.SpawnItem(DropItem, transform.position, DropAmount);
        }

        Destroy(gameObject);
    }
}
