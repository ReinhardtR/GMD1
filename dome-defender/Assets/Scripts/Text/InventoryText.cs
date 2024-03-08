using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private PlayerInventory inventory;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerInventory>();

        inventory.OnItemsChange += UpdateText;
        UpdateText();
    }

    void OnEnable()
    {
        if (inventory) inventory.OnItemsChange += UpdateText;
    }

    void OnDisable()
    {
        if (inventory) inventory.OnItemsChange -= UpdateText;
    }

    private void UpdateText()
    {
        string text = "Inventory:\n";
        foreach (var item in inventory.Items)
        {
            text += $"{item.Key}: {item.Value}\n";
        }
        textMesh.text = text;
    }
}
