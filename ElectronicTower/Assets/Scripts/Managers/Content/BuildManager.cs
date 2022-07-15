
using UnityEngine;
using UnityEngine.Events;

public class BuildManager
{
    private TurretShopData _turretToBuild;

    public bool CanBuild { get { return _turretToBuild != null; }}
    
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
        // TODO : 터렛을 건설했을 시 변화될 UI를 이벤트에 넣어야 함.
        //Managers.Resource.Instantiate(, node.transform);

        return turret;
    }
}
