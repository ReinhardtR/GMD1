using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public Dictionary<Item, int> Items;
    public event Action OnItemsChangedEvent;

    public void OnEnable()
    {
        Items ??= new Dictionary<Item, int>();
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (Items.ContainsKey(item))
        {
            Items[item] += amount;
        }
        else
        {
            Items.Add(item, amount);
        }
        OnItemsChangedEvent?.Invoke();
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        if (Items.ContainsKey(item))
        {
            Items[item] -= amount;
            if (Items[item] <= 0)
            {
                Items.Remove(item);
            }
            OnItemsChangedEvent?.Invoke();
        }
    }

    public void RemoveAll()
    {
        if (Items.Count == 0)
        {
            return;
        }

        Items.Clear();
        OnItemsChangedEvent?.Invoke();
    }
}
