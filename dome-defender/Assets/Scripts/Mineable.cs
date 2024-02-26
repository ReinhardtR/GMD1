using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Mineable : MonoBehaviour
{
    private Health health;
    public event Action OnMineEvent;

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeathEvent += () => Destroy(gameObject);
    }

    public void OnMine(int damage)
    {
        Debug.Log("Mineable hit for: " + damage + " damage.");
        health.TakeDamage(damage);
        OnMineEvent?.Invoke();
    }
}