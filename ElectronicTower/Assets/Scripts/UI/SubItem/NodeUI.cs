using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeUI : UI_Base
{
    private Node _target;

    public enum EButton { Upgrade, Sell }
    public enum EText { UpgradeText , SellText }

    public override void Init()
    {
        BindUI();
        gameObject.SetActive(false);
    }

    void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Text>(typeof(EText));

        BindEvent(GetButton((int)EButton.Upgrade).gameObject, (PointerEventData data) => { Upgrade(); });
        BindEvent(GetButton((int)EButton.Sell).gameObject, (PointerEventData data) => { Sell(); });
    }

    public void SetTarget(Node target)
    {
        if (target == null) return;
        if (_target != null)
        {
            Hide();
            return;
        }

        _target = target;
        
        if (_target.TurretType == Define.ETurretType.PowerPole || _target.UpgradeStep >= _target.ShopData.MaxUpgrade)
            GetButton((int)EButton.Upgrade).gameObject.SetActive(false);
        else
            GetButton((int)EButton.Upgrade).gameObject.SetActive(true);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _target = null;
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        Managers.Build.DeselectNode();
    }

    public void Sell()
    {
        _target.SellTurret();
        Managers.Build.DeselectNode();
    }
}
