using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSystem : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public Inventory Inventory;
    public GameObject towerWeapon;

    [Header("Upgrades")]
    public Upgrade[] upgrades;

    private TowerWeaponController towerWeaponController;

    void Awake()
    {
        towerWeaponController = towerWeapon.GetComponent<TowerWeaponController>();
    }

    void OnEnable()
    {
        RenderUpgradeButtons();
    }

    public bool CanAffordUpgrade(int upgradeIndex)
    {
        Upgrade upgrade = upgrades[upgradeIndex];

        foreach (UpgradeCost cost in upgrade.Costs)
        {
            if (!Inventory.Items.ContainsKey(cost.Item) || Inventory.Items[cost.Item] < cost.Amount)
            {
                return false;
            }
        }

        return true;
    }

    public void UpgradeWeapon(int upgradeIndex)
    {
        if (!CanAffordUpgrade(upgradeIndex))
        {
            return;
        }

        Upgrade upgrade = upgrades[upgradeIndex];

        foreach (UpgradeCost cost in upgrade.Costs)
        {
            Inventory.RemoveItem(cost.Item, cost.Amount);
        }

        towerWeaponController.Damage += upgrade.DamageIncrease;
        towerWeaponController.FireRate = (1 - upgrade.FireRateIncrease) * towerWeaponController.FireRate;

        RenderUpgradeButtons();
    }

    private void RenderUpgradeButtons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject buttonToSelect = null;
        for (int i = 0; i < upgrades.Length; i++)
        {
            Upgrade upgrade = upgrades[i];

            GameObject button = Instantiate(ButtonPrefab, transform);
            button.SetActive(false);
            UpgradeButton upgradeButton = button.GetComponent<UpgradeButton>();
            upgradeButton.Upgrade = upgrade;
            upgradeButton.Index = i;
            upgradeButton.ShopSystem = this;
            button.SetActive(true);

            // select first upgrade or first affordable upgrade for navigation
            if (buttonToSelect == null && (i == 0 || CanAffordUpgrade(i)))
            {
                buttonToSelect = button;
            }
        }

        EventSystem.current.SetSelectedGameObject(buttonToSelect);
    }
}
