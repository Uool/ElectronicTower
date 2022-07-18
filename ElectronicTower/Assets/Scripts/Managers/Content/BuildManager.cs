
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
        GameObject turretObject = Managers.Game.TurretSpawn(_turretToBuild.turretPrefab, node.transform);
        turretObject.transform.position = node.GetBuildPosition();
        // TODO : �ͷ��� �Ǽ����� �� ��ȭ�� UI�� �̺�Ʈ�� �־�� ��.
        //Managers.Resource.Instantiate(, node.transform);

        return turretObject;
    }
}
