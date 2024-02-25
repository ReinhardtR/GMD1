using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    public event Action OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        if (damage >= currentHealth)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
            return;
        }
        
        currentHealth -= damage;
    }
}
