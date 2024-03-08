using System.Collections.Generic;
using UnityEngine;

public static class ItemSpawner
{
    private static readonly GameObject itemPrefab = Resources.Load<GameObject>("Item");

    public static IEnumerable<GameObject> SpawnItem(ItemType itemType, Vector3 position, int amount)
    {
        GameObject[] items = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            items[i] = SpawnItem(itemType, position);
        }
        return items;
    }

    public static GameObject SpawnItem(ItemType itemType, Vector3 position)
    {
        GameObject item = Object.Instantiate(itemPrefab, position, Quaternion.identity);

        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        Vector2 initialForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100;
        rb.AddForce(initialForce);

        Pickupable pickupable = item.GetComponent<Pickupable>();
        pickupable.ItemType = itemType;

        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.color = itemType.Color;

        return item;
    }
}
