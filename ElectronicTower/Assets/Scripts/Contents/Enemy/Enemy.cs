using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable), typeof(Health))]
public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    private Health _health;
    private Transform _target;
    private int _wavePointIndex = 0;

    private readonly float minDistance = 0.2f;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.onDie += Die;
    }

    private void OnEnable()
    {
        _target = WayPoints.points[0];
        _health.SetMaxHealth(enemyData.MaxHp);
        _wavePointIndex = 0;
    }

    void Update()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        transform.Translate(dir * enemyData.MoveSpeed * Time.deltaTime, Space.World);

        if ((_target.position - transform.position).sqrMagnitude < minDistance * minDistance)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (_wavePointIndex >= WayPoints.points.Length - 1)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }

        _wavePointIndex++;
        _target = WayPoints.points[_wavePointIndex];
    }

    void Die()
    {
        // TODO: 사라지는 이팩트 발동

        Managers.Resource.Destroy(gameObject);
    }
}
