using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public ItemType ItemType { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInventory>().AddItem(ItemType);
            Destroy(gameObject);
        }
    }
}
