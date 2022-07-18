using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/PowerPole", fileName = "PowerPoleData")]
public class PowerPoleData : ScriptableObject
{
    [SerializeField] private float _radius;

    public float Radius { get { return _radius; } }
}
