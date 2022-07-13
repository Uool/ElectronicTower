
using UnityEngine;

public class BuildManager
{
    
    private GameObject _turretToBuild;

    [HideInInspector] public GameObject[] turretPrefabs;

    public void Init()
    {
        string[] turretNames = System.Enum.GetNames(typeof(Define.ETurretType));
        turretPrefabs = new GameObject[turretNames.Length];
        for (int i = 0; i < turretNames.Length; i++)
        {
            turretPrefabs[i] = Managers.Resource.Load<GameObject>($"Prefabs/Turret/{turretNames[i]}");
        }
    }

    public GameObject GetTurretToBuild() { return _turretToBuild; }
    
    public void SetTurretToBuild(GameObject turret) { _turretToBuild = turret; }
}
