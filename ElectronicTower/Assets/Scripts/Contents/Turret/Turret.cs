using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretData turretData;
    public Transform targetAimBase;
    public Transform partToRotate;
    public Transform firePoint;

    [HideInInspector] public Transform target;
    private float _fireCountDown = 1f;
    private ParticleSystem _projectile;

    // Start is called before the first frame update
    void Start()
    {
        _projectile = Managers.Resource.Instantiate(turretData.projectile.gameObject, firePoint).GetComponent<ParticleSystem>();
        _projectile.GetComponentInChildren<ProjectileEffect>().Init(gameObject, turretData.Damage, turretData.Type);

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretData.Range);
    }

    // Todo : 조금 더 생각해보자..
    void Shoot()
    {
        _projectile.Play();
        // TODO : 사운드
    }

    #region Function

    void UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, turretData.Range);
        float shortDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        if (colliders.Length == 0)
        {
            target = null;
            return;
        }

        foreach (Collider enemy in colliders)
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
        Vector3 dir = (target.position - targetAimBase.position);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretData.TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    #endregion
}
