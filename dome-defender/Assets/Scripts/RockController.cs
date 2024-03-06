using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mineable))]
[RequireComponent(typeof(SpriteRenderer))]
public class RockController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Health health;
    private Mineable mineable;

    void Awake()
    {
        health = GetComponent<Health>();
        mineable = GetComponent<Mineable>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SetColor(mineable.RockType.StartColor);
    }

    void OnEnable()
    {
        if (health) health.OnDamageEvent += OnDamage;
    }

    void OnDisable()
    {
        if (health) health.OnDamageEvent -= OnDamage;
    }

    public void OnDamage(int damage)
    {
        Color newColor = Color.Lerp(
            mineable.RockType.StartColor,
            mineable.RockType.EndColor,
            1f - (float)health.CurrentHealth / health.MaxHealth
        );

        SetColor(newColor);
    }

    public void SetColor(Color color)
    {
        sprite.color = color;
        sprite.material.color = color;
    }
}
