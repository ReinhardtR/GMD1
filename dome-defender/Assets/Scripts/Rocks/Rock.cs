using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Rock", menuName = "ScriptableObjects/Rock")]
public class Rock : ScriptableObject
{
    public string Name;
    public int MaxHealth;
    [Range(0, 1)]
    public float SpawnChance;
    public int DropAmount;
    public Item DropItem;
    public Sprite Sprite;
    public RockSpriteHander SpriteHandler;
}
