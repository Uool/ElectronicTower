using UnityEngine;

[CreateAssetMenu(menuName = "Object/Enemy", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public void EnemyDataInit(string[] data)
    {
        fileName = data[0];
        _maxHp = float.Parse(data[1]);
        _moveSpeed = float.Parse(data[2]);
        _money = int.Parse(data[3]);
        _damage = int.Parse(data[4]);
    }

    public string fileName;

    [SerializeField] private float _maxHp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _money;
    [SerializeField] private int _damage;

    public float MaxHp { get { return _maxHp; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public int Money { get { return _money; } }
    public int Damage { get { return _damage; } }
}
