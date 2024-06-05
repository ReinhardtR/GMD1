# Introduction

The first milestone of my Game is the Upgrades System. This includes the following:

- Upgrades Menu
- Upgrades Buying (spending resources)
- Base Upgrades (Shield & Weapons)
- Mining Upgrades (Movement & Lasers)

# Upgrades Menu

I could spend a lot of time making a very pretty and complicated UI, but this is an MVP so I have kept it simple. The Upgrades Menu is simply a Panel using a Grid Layout Group that contains a button for each upgrade.

To open the menu the player moves onto the Shop Opener in the base (a pink square) and presses the interact button. This opens the menu which can be navigated using the movement buttons.

The buttons are created by the Shop System component, that is connected to the Shop UI.

In a method called `RenderUpgradeButtons`, I create the buttons and set their needed values.

```csharp
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
```

I delete the buttons first, so that I can recreate them with the right state. E.g. if the player can afford the upgrade.

The actual logic of the button is in a `UpgradeButton` script that is attached to a upgrade button prefab.

This component creates all the text for the button, e.g. stats, name and cost. It also handles the click event and the interactable state.

# Upgrades Buying

To check if the player can afford the upgrade, I simply check the resources the player currently has in it's inventory compared to the cost of the upgrade.

An upgrade can cost multiple resources, so I loop through all the costs and check if the player has enough of each resource.

```csharp
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
```

# Applying Upgrades

I didn't have the time to implement ugprades for both the tower and the player, so I decided to implement a simple upgrade system for jsut the tower weapon.

This is the method that applies an upgrade:

```csharp
 public void UpgradeWeapon(int upgradeIndex)
{
    // check if player can afford upgrade
    if (!CanAffordUpgrade(upgradeIndex))
    {
        return;
    }

    Upgrade upgrade = upgrades[upgradeIndex];

    // remove resources
    foreach (UpgradeCost cost in upgrade.Costs)
    {
        Inventory.RemoveItem(cost.Item, cost.Amount);
    }

    // apply upgrade
    towerWeaponController.Damage += upgrade.DamageIncrease;
    towerWeaponController.FireRate = (1 - upgrade.FireRateIncrease) * towerWeaponController.FireRate;

    // re-render the buttons
    RenderUpgradeButtons();
}
```

Mutating the damage and firerate of the tower weapon directly is not ideal, but for this MVP it works.

Ideally I would have some sort of ScriptableObject for each upgrade that is stored in a "player upgrade inventory", or something of that sort, that can be dynamically loaded and applied.

This would allow for a more dynamic and flexible system, where upgrades can be added and removed easily.
