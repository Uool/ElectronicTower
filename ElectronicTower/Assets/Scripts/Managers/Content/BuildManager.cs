
using UnityEngine;
using UnityEngine.Events;

public class BuildManager
{
    private TurretShopData _turretToBuild;

    public bool CanBuild { get { return _turretToBuild != null; }}
    public Define.ETurretType turretType;
    
    public void SelectTurretToBuild(TurretShopData turretData)
    {
        if (Managers.Player.money >= turretData.cost)
        {
            _turretToBuild = turretData;
        }
        else
        {
            _turretToBuild = null;
            return;
        }
    }

    public GameObject BuildTurretOn(Node node)
    {
        Managers.Player.money -= _turretToBuild.cost;
        GameObject turretObject = null;
        if (turretType == Define.ETurretType.PowerPole)
            turretObject = Managers.Game.PowerPoleSpawn(_turretToBuild.turretPrefab, node.transform);
        else
            turretObject = Managers.Game.TurretSpawn(_turretToBuild.turretPrefab, node.transform);

        turretObject.transform.position = node.GetBuildPosition();
        _turretToBuild = null;

        // TODO : 터렛을 건설했을 시 변화될 UI를 이벤트에 넣어야 함.
        //Managers.Resource.Instantiate(, node.transform);

        return turretObject;
    }
}
