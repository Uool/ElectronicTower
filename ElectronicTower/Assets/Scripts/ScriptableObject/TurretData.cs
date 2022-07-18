using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Object/Turret", fileName ="TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject projectile;

    [SerializeField] private Define.ETurretType _type;
    [SerializeField] private float _range;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _damage;

    public Define.ETurretType Type { get { return _type; } }
    public float Range { get { return _range; } }
    public float TurnSpeed { get { return _turnSpeed; } }
    public float FireRate { get { return _fireRate; } }
    public float Damage { get { return _damage; } }
}
