using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/Enemy", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _moveSpeed;

    public float MaxHp { get { return _maxHp; } }
    public float MoveSpeed { get { return _moveSpeed; } }
}
