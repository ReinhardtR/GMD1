using UnityEngine;

public class Collector : MonoBehaviour
{
    public Inventory Inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Collectible>(out var collectible))
        {
            if (Inventory)
            {
                Inventory.AddItem(collectible.Item);
            }

            collectible.Collect();
        }
    }
}
