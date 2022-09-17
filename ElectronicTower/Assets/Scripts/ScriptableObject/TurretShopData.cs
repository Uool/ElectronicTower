using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Turret", fileName = "TurretShop")]
public class TurretShopData : ScriptableObject
{
    public void TurretShopDataInit(string[] data)
    {
        turretPrefab = new GameObject[3];

        fileName = data[0];       
        turretPrefab[0] = Resources.Load<GameObject>($"Prefabs/Turret/{data[1]}");
        turretPrefab[1] = Resources.Load<GameObject>($"Prefabs/Turret/{data[2]}");
        turretPrefab[2] = Resources.Load<GameObject>($"Prefabs/Turret/{data[3]}");
        _cost = int.Parse(data[4]);
        _upgradeCost = float.Parse(data[5]);
        _maxUpgrade = int.Parse(data[6]);
    }

    public string fileName;
    public GameObject[] turretPrefab;

    [SerializeField] private int _cost;
    [SerializeField] private float _upgradeCost;
    [SerializeField] private int _maxUpgrade;

    public int Cost { get { return _cost; } }
    public float UpgradeCost { get { return _upgradeCost; } }
    public int MaxUpgrade { get { return _maxUpgrade; } }
}
