using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mineable))]
[RequireComponent(typeof(SpriteRenderer))]
public class RockController : MonoBehaviour
{
    public Rock Rock;
    private SpriteRenderer spriteRenderer;
    private Health health;
    private Mineable mineable;

    void Awake()
    {
        health = GetComponent<Health>();
        mineable = GetComponent<Mineable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        mineable.DropItem = Rock.DropItem;
        mineable.DropAmount = Rock.DropAmount;

        health.SetMaxHealth(Rock.MaxHealth);

        spriteRenderer.sprite = Rock.Sprite;

        Terrain.OnRockChangedEvent += UpdateRockSprite;
        UpdateRockSprite(transform.position);
    }

    void OnEnable()
    {
        Terrain.OnRockChangedEvent += UpdateRockSprite;

        health.OnDamageEvent += OnDamage;
        health.OnDeathEvent += OnDeath;
    }

    void OnDisable()
    {
        Terrain.OnRockChangedEvent -= UpdateRockSprite;

        health.OnDamageEvent -= OnDamage;
        health.OnDeathEvent -= OnDeath;
    }

    public void OnDamage(int damage)
    {
        UpdateCrackingMaterial();
    }

    private void OnDeath()
    {
        Terrain.RemoveRock(transform.position);
    }

    private void UpdateCrackingMaterial()
    {
        spriteRenderer.material.SetFloat(
            "_Percentage",
            1 - health.GetHealthPercentage()
        );
    }

    private void UpdateRockSprite(Vector2 changedRockPos)
    {
        if (
            destroyCancellationToken.IsCancellationRequested ||
            Rock.SpriteHandler == null ||
            Vector2.Distance(transform.position, changedRockPos) > 1f
        )
        {
            return;
        }

        spriteRenderer.sprite = Rock.SpriteHandler.GetRockSprite(transform.position);
    }
}
