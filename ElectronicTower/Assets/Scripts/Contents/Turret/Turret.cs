using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretData turretData;
    public Transform partToRotate;
    public Transform firePoint;

    [HideInInspector] public Transform target;
    private float _fireCountDown = 1f;

    // Start is called before the first frame update
    void Start()
    {
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

    #region virtual

    protected virtual void Shoot() { }

    #endregion

    #region Function

    // TODO: ��� ���� ã��? �浹üũ ����?? (���� ����� �Ź� ã�µ� ���ϰ� ���� �� �ʹ�.
    void UpdateTarget()
    {

    }

    // Ÿ���� �����Ѵ�.
    void UpdateTurretHead()
    {
        Vector3 dir = (target.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretData.TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    

    #endregion
}
