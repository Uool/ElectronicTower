using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : UI_Base
{
    private Node _target;

    public override void Init()
    {
        
    }

    public void SetTarget(Node target)
    {
        _target = target;
        _target.transform.position = target.GetBuildPosition();
    }

    public void Hide()
    {
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
