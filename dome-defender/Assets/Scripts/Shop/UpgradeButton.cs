using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Upgrade Upgrade;
    public int Index;
    public ShopSystem ShopSystem;

    private Button button;
    private TMP_Text buttonText;

    void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
    }


    void OnEnable()
    {
        if (Upgrade == null || ShopSystem == null)
        {
            button.interactable = false;
            buttonText.text = "";
            return;
        }

        button.interactable = ShopSystem.CanAffordUpgrade(Index);
        button.onClick.AddListener(() => ShopSystem.UpgradeWeapon(Index));

        var textBuilder = new StringBuilder();

        // Name
        textBuilder
            .Append(Upgrade.Name)
            .Append("\n");
        textBuilder
            .Append("\n");

        // Costs
        foreach (UpgradeCost cost in Upgrade.Costs)
        {
            textBuilder
                .Append(cost.Item.name)
                .Append(": -")
                .Append(cost.Amount)
                .Append("\n");
        }
        textBuilder.Append("\n");

        // Stats
        textBuilder
            .Append("Damage: +")
            .Append(Upgrade.DamageIncrease)
            .Append("\n");
        textBuilder
            .Append("Fire Rate: +")
            .Append((Upgrade.FireRateIncrease * 100).ToString("0"))
            .Append("%")
            .Append("\n");

        textBuilder.Append("\n");

        buttonText.text = textBuilder.ToString();
    }
}
