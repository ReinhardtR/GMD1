using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class RockController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Health health;

    // #5F5F5F
    private readonly Color startColor = new(0.373f, 0.373f, 0.373f);
    // #444444
    private readonly Color endColor = new(0.267f, 0.267f, 0.267f);

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDamageEvent += OnDamage;

        sprite = GetComponent<SpriteRenderer>();
        sprite.material.color = startColor;
    }

    void OnDestroy()
    {
        health.OnDamageEvent -= OnDamage;
    }

    public void OnDamage(int damage)
    {
        float t = 1f - (float)health.CurrentHealth / health.MaxHealth;
        sprite.material.color = Color.Lerp(startColor, endColor, t);
        // Debug.Log($"Current Health: {health.CurrentHealth}/{health.MaxHealth} ({t:P})");
    }
}
