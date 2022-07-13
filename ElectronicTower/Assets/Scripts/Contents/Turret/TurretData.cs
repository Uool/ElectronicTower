using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Object/Turret", fileName ="TurretData")]
public class TurretData : ScriptableObject
{
    [SerializeField] private float _range;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _fireRate = 1f;

    public float Range { get { return _range; } }
    public float TurnSpeed { get { return _turnSpeed; } }
    public float FireRate { get { return _fireRate; } }
}
