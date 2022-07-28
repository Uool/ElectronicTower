using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Poolable))]
public class PowerPole : MonoBehaviour
{
    public PowerPoleData poleData;
    public Transform electroLineTr;
    public GameObject electroArea;

    private LineRenderer _lineRenderer;
    private List<Turret> _linkedTurret = new List<Turret>();
    private List<PowerPole> _linkedPowerPole = new List<PowerPole>();
    private float _offsetHeight = 0.5f;
    private Vector3 _lineOriginPos = Vector3.zero;

    [HideInInspector] public bool isLinked;
    [HideInInspector] public bool isSupplied;
    [HideInInspector] public Transform myNode;
    public UnityAction linkedAction;


    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, poleData.Radius);
    }

    public void Init()
    {
        linkedAction += LinkedTurret;
        linkedAction += LinkedPowerPole;

        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        if (myNode != null)
        {
            _lineOriginPos = electroLineTr.position + Vector3.up * _offsetHeight;
            _lineRenderer.SetPosition(0, myNode.TransformPoint(electroLineTr.position + Vector3.up * _offsetHeight));
            _lineRenderer.SetPosition(1, myNode.TransformPoint(electroLineTr.position + Vector3.up * _offsetHeight));
        }
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
                    //Vector3 localPos = electroLineTr.position * transform.localScale;
                    turret.ConnectedPowerPole(myNode.TransformPoint(_lineOriginPos));
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
                    powerPole.ConnectedPowerPole(myNode.TransformPoint(electroLineTr.position));
                    powerPole.isLinked = true;
                }
            }
        }
    }

    public void ActiveElectroArea(bool isOn)
    {
        electroArea.SetActive(isOn);
    }

    public void ConnectedPowerPole(Vector3 position)
    {
        _lineRenderer?.SetPosition(1, position);
    }

    public void DisConnectedPowerPole()
    {
        _lineRenderer?.SetPosition(1, myNode.TransformPoint(electroLineTr.position));
    }
}
