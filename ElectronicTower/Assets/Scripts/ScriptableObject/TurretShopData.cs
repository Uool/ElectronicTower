using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Turret", fileName = "TurretShop")]
public class TurretShopData : ScriptableObject
{
    public GameObject turretPrefab;
    public int cost;
}
