using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Poolable))]
public class PowerPole : MonoBehaviour
{
    public PowerPoleData poleData;
    public GameObject electroArea;

    private LineRenderer _lineRenderer;
    private List<Turret> _linkedTurret = new List<Turret>();
    private List<PowerPole> _linkedPowerPole = new List<PowerPole>();

    [HideInInspector] public bool isLinked;
    [HideInInspector] public bool isSupplied;
    public UnityAction linkedAction;


    private void OnEnable()
    {
        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Init()
    {
        linkedAction += LinkedTurret;
        linkedAction += LinkedPowerPole;
    }

    public void LinkedTurret()
    {
        foreach (var turret in Managers.Game.turretList)
        {
            float distance = (turret.transform.position - transform.position).magnitude;
            if (poleData.Radius > distance)
            {
                if (_linkedTurret.Contains(turret) == false)
                {
                    _linkedTurret.Add(turret);
                    turret.isLinked = true;
                }    
            }
        }
    }

    public void LinkedPowerPole()
    {
        foreach (var powerPole in Managers.Game.powerPoleList)
        {
            float distance = (powerPole.transform.position - transform.position).magnitude;
            if (poleData.Radius > distance)
            {
                if (_linkedPowerPole.Contains(powerPole) == false)
                {
                    _linkedPowerPole.Add(powerPole);
                    powerPole.isLinked = true;
                }
            }
        }
    }

    public void ActiveElectroArea(bool isOn)
    {
        electroArea.SetActive(isOn);
    }
}
