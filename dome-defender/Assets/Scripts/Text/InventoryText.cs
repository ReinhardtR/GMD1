using TMPro;
using UnityEngine;

public class InventoryText : MonoBehaviour
{
    public Inventory Inventory;

    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText();
    }

    void OnEnable()
    {
        if (Inventory)
        {
            Inventory.OnItemsChangedEvent += UpdateText;
        }
    }

    void OnDisable()
    {
        if (Inventory)
        {
            Inventory.OnItemsChangedEvent -= UpdateText;
        }
    }

    private void UpdateText()
    {
        if (Inventory == null)
        {
            return;
        }

        string text = "Inventory\n";
        foreach (var (item, amount) in Inventory.Items)
        {
            text += $"{item.Name}: {amount}\n";
        }

        textMesh.text = text;
    }
}
