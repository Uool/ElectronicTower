using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Object/Turret", fileName ="TurretData")]
public class TurretData : ScriptableObject
{
    public void TurretDataInit(string[] data)
    {
        _turretName = data[0];
        projectile = Resources.Load<GameObject>($"Prefabs/Turret/{data[1]}");
        _type = (Define.ETurretType)int.Parse(data[2]);
        _level = int.Parse(data[3]);
        _range = float.Parse(data[4]);
        _turnSpeed = float.Parse(data[5]);
        _fireRate = float.Parse(data[6]);
        _slowMultiplier = float.Parse(data[7]);
        _damage = float.Parse(data[8]);
    }

    public GameObject projectile;

    [SerializeField] private string _turretName;
    [SerializeField] private Define.ETurretType _type;
    [SerializeField] private int _level;
    [SerializeField] private float _range;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _slowMultiplier;
    [SerializeField] private float _damage;

    public string TurretName { get { return _turretName; } }
    public Define.ETurretType Type { get { return _type; } }
    public int Level { get { return _level; } }
    public float Range { get { return _range; } }
    public float TurnSpeed { get { return _turnSpeed; } }
    public float FireRate { get { return _fireRate; } }
    public float SlowMultiplier { get { return _slowMultiplier; } }
    public float Damage { get { return _damage; } }
}
