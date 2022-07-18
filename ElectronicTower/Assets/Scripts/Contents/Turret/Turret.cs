using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public abstract class Turret : MonoBehaviour
{
    public TurretData turretData;
    public Transform targetAimBase;
    public Transform partToRotate;
    public Transform firePoint;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool isLinked;


    private float _fireCountDown = 1f;
    protected abstract void Shoot();

    public virtual void Init()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected void TurretUpdate()
    {
        if (target == null)
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

    protected void UpdateTarget()
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

    protected void UpdateTurretHead()
    {
        Vector3 dir = (target.position - targetAimBase.position);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretData.TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    #endregion
}
