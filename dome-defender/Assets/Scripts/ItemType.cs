using UnityEngine;

public class ItemType : Enumeration<ItemType>
{
    public static readonly ItemType Copper = new(2,
        "Copper",
        color: new(1f, 0.647f, 0f),
        weight: 1f
    );
    public static readonly ItemType Iron = new(3,
        "Iron",
        color: new(0.384f, 0.255f, 0.161f),
        weight: 5f
    );
    public static readonly ItemType Gold = new(4,
        "Gold",
        color: new(1f, 0.843f, 0f),
        weight: 10f
    );
    public static readonly ItemType Diamond = new(5,
        "Diamond",
        color: new(0f, 0.749f, 1f),
        weight: 1f
    );

    public Color Color { get; protected set; }
    public float Weight { get; protected set; }

    protected ItemType(int value, string name, Color color, float weight) : base(value, name)
    {
        Color = color;
        Weight = weight;
    }
}
