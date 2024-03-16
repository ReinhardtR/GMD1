using System.Collections.Generic;
using UnityEngine;

public static class ItemSpawner
{
    private static readonly GameObject itemPrefab = Resources.Load<GameObject>("Item");

    public static IEnumerable<GameObject> SpawnItem(Item item, Vector3 position, int amount)
    {
        GameObject[] items = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            items[i] = SpawnItem(item, position);
        }
        return items;
    }

    public static GameObject SpawnItem(Item item, Vector3 position)
    {
        GameObject itemObject = Object.Instantiate(itemPrefab, position, Quaternion.identity);

        Rigidbody2D rb = itemObject.GetComponent<Rigidbody2D>();
        Vector2 initialForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100;
        rb.AddForce(initialForce);

        Collectible collectible = itemObject.GetComponent<Collectible>();
        collectible.Item = item;

        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = item.Color;

        return itemObject;
    }
}
