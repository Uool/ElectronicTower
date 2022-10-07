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
    private float _offsetHeight = 0.5f;
    private Vector3 _lineOriginPos = Vector3.zero;

    [HideInInspector] public Transform myNode;
    [HideInInspector] public Vector3 originPos;
    [HideInInspector] public bool isSupplied;
    public UnityAction linkedAction;

    public void Init()
    {
        linkedAction += LinkedTurret;

        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        if (myNode != null)
        {
            _lineOriginPos = electroLineTr.position + Vector3.up * _offsetHeight;
            _lineRenderer.SetPosition(0, myNode.TransformPoint(electroLineTr.position + Vector3.up * _offsetHeight));
            _lineRenderer.SetPosition(1, myNode.TransformPoint(electroLineTr.position + Vector3.up * _offsetHeight));
        }
        float localScale = poleData.Radius * 2f;
        electroArea.transform.localScale = new Vector3(localScale, 0.1f, localScale);

        originPos = myNode.position + new Vector3(0, myNode.position.y + _offsetHeight, 0);
        ActiveElectroArea(Managers.Game.isPowerPoleArea);
    }

    public void LinkedTurret()
    {
        foreach (var turret in Managers.Game.turretList)
        {
            float distance = (turret.originPos - originPos).magnitude;
            if (poleData.Radius > distance)
            {
                if (_linkedTurret.Contains(turret) == false)
                {
                    _linkedTurret.Add(turret);
                    //Vector3 localPos = electroLineTr.position * transform.localScale;
                    turret.ConnectedPowerPole(myNode.TransformPoint(_lineOriginPos));
                    turret.isLinked = true;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, poleData.Radius);
    }
}
