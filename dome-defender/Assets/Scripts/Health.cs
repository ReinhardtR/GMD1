using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; set; }
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

        if (damage >= CurrentHealth)
        {
            CurrentHealth = 0;
            OnDamageEvent?.Invoke(damage);
            OnDeathEvent?.Invoke();
            return;
        }

        CurrentHealth -= damage;
        OnDamageEvent?.Invoke(damage);
    }
}
