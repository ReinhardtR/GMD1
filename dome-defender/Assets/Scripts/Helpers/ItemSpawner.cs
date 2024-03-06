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

        Vector2 initialForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100;
        item.GetComponent<Rigidbody2D>().AddForce(initialForce);

        item.GetComponent<SpriteRenderer>().color = itemType.Color;

        return item;
    }
}
