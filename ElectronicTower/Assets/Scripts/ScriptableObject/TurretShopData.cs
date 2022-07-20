using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Turret", fileName = "TurretShop")]
public class TurretShopData : ScriptableObject
{
    public GameObject[] turretPrefab;

    private int _cost;
    private int _upgradeCost;

    public int Cost { get { return _cost; } }
    public int UpgradeCost { get { return _upgradeCost; } }

}
