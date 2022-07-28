using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public abstract class Turret : MonoBehaviour
{
    public TurretData turretData;
    public Transform targetAimBase;
    public Transform partToRotate;
    public Transform[] firePoints;

    [HideInInspector] public Transform target;
    [HideInInspector] public Transform myNode;
    [HideInInspector] public bool isLinked;

    private LineRenderer _lineRenderer;
    private float _fireCountDown = 1f;
    private float _offsetHeight = 0.5f;
    private Vector3 _lineOriginPos = Vector3.zero;

    protected abstract void Shoot();

    public virtual void Init()
    {
        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        if (myNode != null)
        {
            _lineOriginPos = targetAimBase.position + Vector3.up * _offsetHeight;
            _lineRenderer.SetPosition(0, myNode.TransformPoint(_lineOriginPos));
            _lineRenderer.SetPosition(1, myNode.TransformPoint(_lineOriginPos));
        }

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected void TurretUpdate()
    {
        if (target == null && isLinked == false)
            return;

        UpdateTurretHead();

        if (_fireCountDown <= 0f)
        {
            Shoot();
            _fireCountDown = 1f / turretData.FireRate;
        }
        _fireCountDown -= Time.deltaTime;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretData.Range);
    }    

    #region Function

    void UpdateTarget()
    {
        List<Enemy> enemyList = Managers.Game.enemyList;
        float shortDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        if (enemyList.Count == 0)
        {
            target = null;
            return;
        }
        
        foreach (Enemy enemy in enemyList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }
        if (nearestEnemy != null && shortDistance <= turretData.Range)
            target = nearestEnemy;
    }

    void UpdateTurretHead()
    {
        if (target == null) return;

        Vector3 dir = (target.position - targetAimBase.position);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretData.TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }


    public void ConnectedPowerPole(Vector3 position)
    {
        _lineRenderer.SetPosition(0, myNode.TransformPoint(_lineOriginPos));
        _lineRenderer?.SetPosition(1, position);
    }

    public void DisConnectedPowerPole()
    {
        _lineRenderer?.SetPosition(1, myNode.TransformPoint(_lineOriginPos));
    }
    #endregion
}
