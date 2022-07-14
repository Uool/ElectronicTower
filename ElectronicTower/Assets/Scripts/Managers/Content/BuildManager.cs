
using UnityEngine;
using UnityEngine.Events;

public class BuildManager
{
    [HideInInspector] public GameObject[] turretPrefabs;
    public bool CanBuild { get { return _turretToBuild != null; }}

    public UnityAction OnPurchase;
    private TurretShopData _turretToBuild;
    
    public void SelectTurretToBuild(TurretShopData turretData)
    {
        if (Managers.Player.money >= turretData.cost)
        {
            _turretToBuild = turretData;
        }
        else
        {
            return;
        }
    }

    public GameObject BuildTurretOn(Node node)
    {
        Managers.Player.money -= _turretToBuild.cost;
        GameObject turret = Managers.Resource.Instantiate(_turretToBuild.turretPrefab, node.transform);
        turret.transform.position = node.GetBuildPosition();

        if (OnPurchase != null) OnPurchase.Invoke();

        return turret;
    }
}
