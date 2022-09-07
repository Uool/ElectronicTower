using UnityEngine;

[CreateAssetMenu(menuName = "Object/Enemy", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _money;

    public float MaxHp { get { return _maxHp; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public int Money { get { return _money; } }
}
