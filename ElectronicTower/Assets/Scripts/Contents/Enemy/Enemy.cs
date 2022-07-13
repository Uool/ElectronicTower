using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] EnemyData enemyData;

    private Transform _target;
    private int _wavePointIndex = 0;

    private readonly float minDistance = 0.2f;

    void Start()
    {
        _target = WayPoints.points[0];
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
}
