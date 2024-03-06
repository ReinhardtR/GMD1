using UnityEngine;

public class RockType : Enumeration<RockType>
{
    public static readonly RockType Stone = new(1,
        "Stone",
        startColor: new(0.5f, 0.5f, 0.5f),
        endColor: new(0.3f, 0.3f, 0.3f),
        maxHealth: 100,
        spawnChance: 0.70f
    );
    public static readonly RockType Copper = new(2,
        "Copper",
        startColor: new(1f, 0.647f, 0f),
        endColor: new(0.545f, 0.271f, 0.075f),
        maxHealth: 200,
        spawnChance: 0.12f,
        dropItem: ItemType.Copper
    );
    public static readonly RockType Iron = new(3,
        "Iron",
        startColor: new(0.384f, 0.255f, 0.161f),
        endColor: new(0.161f, 0.09f, 0.035f),
        maxHealth: 300,
        spawnChance: 0.10f,
        dropItem: ItemType.Iron
    );
    public static readonly RockType Gold = new(4,
        "Gold",
        startColor: new(1f, 0.843f, 0f),
        endColor: new(0.855f, 0.647f, 0.128f),
        maxHealth: 400,
        spawnChance: 0.07f,
        dropItem: ItemType.Gold
    );
    public static readonly RockType Diamond = new(5,
        "Diamond",
        startColor: new(0f, 0.749f, 1f),
        endColor: new(0f, 0.392f, 1f),
        maxHealth: 500,
        spawnChance: 0.01f,
        dropItem: ItemType.Diamond
    );

    public Color StartColor { get; protected set; }
    public Color EndColor { get; protected set; }
    public int MaxHealth { get; protected set; }
    public float SpawnChance { get; protected set; }
    public int DropAmount { get; protected set; }
    public ItemType DropItem { get; protected set; }

    protected RockType(int value, string name, Color startColor, Color endColor, int maxHealth, float spawnChance, ItemType dropItem = null, int dropAmount = 1) : base(value, name)
    {
        StartColor = startColor;
        EndColor = endColor;
        MaxHealth = maxHealth;
        SpawnChance = spawnChance;
        DropItem = dropItem;
        DropAmount = dropAmount;
    }
}
