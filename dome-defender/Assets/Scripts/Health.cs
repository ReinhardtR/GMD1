using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; private set; }
    public event Action OnDeathEvent;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0) return;

        if (damage >= CurrentHealth)
        {
            CurrentHealth = 0;
            OnDeathEvent?.Invoke();
            return;
        }

        CurrentHealth -= damage;
    }
}
