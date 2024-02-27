using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; private set; }
    public event Action OnDeathEvent;
    public event Action<int> OnDamageEvent;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0) return;

        OnDamageEvent?.Invoke(damage);

        if (damage >= CurrentHealth)
        {
            CurrentHealth = 0;
            OnDeathEvent?.Invoke();
            return;
        }

        CurrentHealth -= damage;
    }
}
