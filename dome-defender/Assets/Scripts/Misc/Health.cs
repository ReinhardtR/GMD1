using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public event Action OnDeathEvent;
    public event Action<int> OnDamageEvent;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public float GetHealthPercentage()
    {
        return (float)CurrentHealth / MaxHealth;
    }

    public void SetMaxHealth(int newMaxHealth, bool resetHealth = true)
    {
        MaxHealth = newMaxHealth;
        if (resetHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

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
