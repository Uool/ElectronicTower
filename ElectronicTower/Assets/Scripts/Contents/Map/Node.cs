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

    public Define.ETurretType TurretType { get; private set; }
    public TurretShopData ShopData { get { return _shopData; } }
    public int UpgradeStep { get { return _upgradeStep; } }

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

        Managers.Player.ConsumeMoney(shopData.Cost);
        GameObject turretObject = null;
        if (Managers.Build.TurretType == Define.ETurretType.PowerPole)
            turretObject = Managers.Game.PowerPoleSpawn(shopData.turretPrefab[_upgradeStep], transform);
        else
            turretObject = Managers.Game.TurretSpawn(shopData.turretPrefab[_upgradeStep], transform);

        turretObject.transform.position = GetBuildPosition();

        _turret = turretObject;
        TurretType = Managers.Build.TurretType;

        // TODO : 터렛을 건설했을 시 변화될 UI를 이벤트에 넣어야 함.
        GameObject buildEffect = Managers.Resource.Instantiate("Effect/BuildPfx", transform);
        Managers.Resource.Destroy(buildEffect, 2f);
        Managers.Sound.Play("Turret/TurretBuild");

        if (Managers.Build.buildEndAction != null)
            Managers.Build.buildEndAction.Invoke();
    }

    public void UpgradeTurret()
    {
        if (Managers.Player.GameMoney >= _shopData.UpgradeCost)
        {
            _upgradeStep++;
            if (_upgradeStep > _shopData.MaxUpgrade)
                return;

            Managers.Sound.Play("Turret/TurretBuild");
            Managers.Resource.Destroy(_turret);
            GameObject turretObject = Managers.Game.TurretSpawn(_shopData.turretPrefab[_upgradeStep], transform);
            turretObject.transform.position = GetBuildPosition();
            _turret = turretObject;
        }
        else
        {
            // TODO: 돈이 없다는 Toast를 띄워야 함

            return;
        }
    }

    public void SellTurret()
    {
        int sellCost = (int)(_shopData.Cost * 0.8f);
        Managers.Player.GetMoney(sellCost);

        Managers.Resource.Destroy(_turret);
        _turret = null;
        _shopData = null;

        // TODO : 판매 시 연출 or 사운드
        GameObject sellEffect = Managers.Resource.Instantiate("Effect/FX_Money_Coins_Burst_01", transform);
        Managers.Sound.Play("Turret/TurretSell");
        sellEffect.transform.position = GetBuildPosition();
        Managers.Resource.Destroy(sellEffect,2f);
    }

    public Vector3 GetBuildPosition() { return transform.position + positionOffset; }
}
