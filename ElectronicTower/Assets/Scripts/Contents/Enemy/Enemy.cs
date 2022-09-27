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
    private float _currentSpeed;

    private readonly float minDistance = 0.2f;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.onDie -= Die;
        _health.onDie += Die;
    }

    private void OnEnable()
    {
        _target = WayPoints.points[0];
        _health.SetMaxHealth(enemyData.MaxHp);
        _wavePointIndex = 0;
        _currentSpeed = enemyData.MoveSpeed;
    }

    private void Start()
    {
        string objectName = gameObject.name;
        enemyData = Managers.Resource.Load<EnemyData>($"ScriptableObject/Enemy/{objectName}");
    }

    void Update()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        transform.Translate(dir * _currentSpeed * Time.deltaTime, Space.World);

        if ((_target.position - transform.position).sqrMagnitude < minDistance * minDistance)
        {
            GetNextWayPoint();
        }
        _currentSpeed = enemyData.MoveSpeed;
    }

    public void SlowSpeed(float percent)
    {
        _currentSpeed = enemyData.MoveSpeed * percent;
    }

    void GetNextWayPoint()
    {
        if (_wavePointIndex >= WayPoints.points.Length - 1)
        {
            Despawn();
            return;
        }

        _wavePointIndex++;
        _target = WayPoints.points[_wavePointIndex];
    }

    void Die()
    {
        // TODO: 사라지는 이팩트 발동
        //Managers.Resource.Instantiate(, transform);
        Managers.Player.GetMoney(enemyData.Money);
        Managers.Game.EnemyDespawn(this);
    }

    void Despawn()
    {
        // TODO: 플레이어 체력이 깎이는 사운드 and 효과
        Managers.Game.EnemyDespawn(this);
        Managers.Player.DamageHealth(enemyData.Damage);
        Managers.Game.GameOver();
    }
}
