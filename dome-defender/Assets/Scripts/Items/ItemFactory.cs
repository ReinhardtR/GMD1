using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    public GameObject ItemPrefab;

    public void CreateItem(Item item, Vector3 position, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CreateItem(item, position);
        }
    }

    public void CreateItem(Item item, Vector3 position)
    {
        GameObject itemObject = Instantiate(ItemPrefab, position, Quaternion.identity);

        Collectible collectible = itemObject.GetComponent<Collectible>();
        collectible.Item = item;

        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = item.Color;
    }
}
