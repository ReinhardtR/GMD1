using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action OnItemsChange;

    public Dictionary<ItemType, int> Items { get; private set; }

    void Awake()
    {
        Items = new Dictionary<ItemType, int>();
    }

    public void AddItem(ItemType itemType, int amount = 1)
    {
        if (Items.ContainsKey(itemType))
        {
            Items[itemType] += amount;
        }
        else
        {
            Items.Add(itemType, amount);
        }

        OnItemsChange?.Invoke();
    }

    public void Clear()
    {
        Items.Clear();
        OnItemsChange?.Invoke();
    }
}
