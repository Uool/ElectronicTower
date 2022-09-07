using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [HideInInspector] public Color nodeColor;
    public Vector3 positionOffset;

    private GameObject _turret;
    private TurretShopData _shopData;
    private Renderer _renderer;
    private BoxCollider _collider;

    private Color _startColor;
    private Color _possibleColor;
    private Color _impossibleColor;

    private int _upgradeStep;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();

        _startColor = Color.white;
        _possibleColor = Color.green;
        _impossibleColor = Color.red;
    }

     void Update()
    {
        if (Input.GetMouseButtonDown(0) && RaycastNode())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_turret != null)
            {
                Managers.Build.SelectNode(this);
                return;
            }

            if (Managers.Build.CanBuild == false)
                return;

            BuildTurret(Managers.Build.GetTurretToBuild());
        }
    }

    bool RaycastNode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == _collider)
                return true;
        }
        return false;
    }

    void BuildTurret(TurretShopData shopData)
    {
        _shopData = shopData;

        Managers.Player.money -= shopData.Cost;
        GameObject turretObject = null;
        if (Managers.Build.TurretType == Define.ETurretType.PowerPole)
            turretObject = Managers.Game.PowerPoleSpawn(shopData.turretPrefab[_upgradeStep], transform);
        else
            turretObject = Managers.Game.TurretSpawn(shopData.turretPrefab[_upgradeStep], transform);

        turretObject.transform.position = GetBuildPosition();
        _turret = turretObject;

        // TODO : �ͷ��� �Ǽ����� �� ��ȭ�� UI�� �̺�Ʈ�� �־�� ��.
        //Managers.Resource.Instantiate(, node.transform);

        if (Managers.Build.buildEndAction != null)
            Managers.Build.buildEndAction.Invoke();
    }

    public void UpgradeTurret()
    {
        if (Managers.Player.money >= _shopData.UpgradeCost)
        {
            _upgradeStep++;
            GameObject turretObject = null;

            Managers.Resource.Destroy(_turret);
            if (Managers.Build.TurretType == Define.ETurretType.PowerPole)
                turretObject = Managers.Game.PowerPoleSpawn(_shopData.turretPrefab[_upgradeStep], transform);
            else
                turretObject = Managers.Game.TurretSpawn(_shopData.turretPrefab[_upgradeStep], transform);

            turretObject.transform.position = GetBuildPosition();
            _turret = turretObject;
        }
        else
        {
            // TODO: ���� ���ٴ� Toast�� ����� ��

            return;
        }
    }

    public void SellTurret()
    {
        // TODO: �� �׼��� ���߿� ����.
        Managers.Player.money += 100;

        Managers.Resource.Destroy(_turret);
        _shopData = null;

        // TODO : �Ǹ� �� ���� or ����
        //Managers.Resource.Instantiate(, node.transform);
    }

    public Vector3 GetBuildPosition() { return transform.position + positionOffset; }
}
