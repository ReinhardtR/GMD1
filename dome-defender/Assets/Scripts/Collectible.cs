using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item Item { get; set; }
    public static event Action<Item> OnCollectedEvent;

    public void Collect()
    {
        OnCollectedEvent?.Invoke(Item);
        Destroy(gameObject);
    }
}
