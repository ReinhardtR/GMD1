using System;
using UnityEngine;

[Serializable]
public class Upgrade
{
    public string Name;
    public int DamageIncrease;
    [Tooltip("Percentage increase")]
    public float FireRateIncrease;
    public UpgradeCost[] Costs;
}

[Serializable]
public class UpgradeCost
{
    public Item Item;
    public int Amount;
}