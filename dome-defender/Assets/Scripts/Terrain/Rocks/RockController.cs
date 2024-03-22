using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class RockController : MonoBehaviour
{
    public TerrainData Terrain;
    public Rock Rock;
    private SpriteRenderer spriteRenderer;
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        health.SetMaxHealth(Rock.MaxHealth);

        spriteRenderer.sprite = Rock.Sprite;

        Terrain.OnRockChangedEvent += UpdateRockSprite;
        UpdateRockSprite(transform.position);
    }

    void OnEnable()
    {
        if (Terrain != null)
        {
            Terrain.OnRockChangedEvent += UpdateRockSprite;
        }

        health.OnDamageEvent += OnDamage;
        health.OnDeathEvent += OnDeath;
    }

    void OnDisable()
    {
        if (Terrain != null)
        {
            Terrain.OnRockChangedEvent -= UpdateRockSprite;
        }

        health.OnDamageEvent -= OnDamage;
        health.OnDeathEvent -= OnDeath;
    }

    private void OnDamage(int damage)
    {
        UpdateCrackingMaterial();
    }

    private void OnDeath()
    {
        Terrain.RemoveRock(transform.position);
        Destroy(gameObject);
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
            !gameObject.activeSelf ||
            Terrain == null ||
            Rock.SpriteHandler == null ||
            Vector2.Distance(transform.position, changedRockPos) > 1f
        )
        {
            return;
        }

        spriteRenderer.sprite = Rock.SpriteHandler.GetRockSprite(Terrain, transform.position);
    }
}
