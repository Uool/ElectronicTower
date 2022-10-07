using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/PowerPole", fileName = "PowerPoleData")]
public class PowerPoleData : ScriptableObject
{
    public void PowerPoleDataInit(string[] data)
    {
        _turretName = data[0];
        _radius = float.Parse(data[1]);
    }

    [SerializeField] private string _turretName;
    [SerializeField] private float _radius;

    public string TurretName { get { return _turretName; } }
    public float Radius { get { return _radius; } }
}
