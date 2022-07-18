﻿using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private GameObject _owner;
    private Define.ETurretType _type;

    private float _damage;
    private string _enemyTag = "Enemy";
    private float _slowMultiplier = 0.2f;

    public void Init(GameObject owner, float damage, float multiplier, Define.ETurretType type)
    {
        _owner = owner;
        _damage = damage;
        _slowMultiplier = multiplier;
        _type = type;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(_enemyTag))
        {
            if (other.GetComponent<Health>())
            {
                if (_type == Define.ETurretType.Laser)
                {
                    other.GetComponent<Health>().TakeDamage(_damage * Time.deltaTime, other);
                    other.GetComponent<Enemy>().SlowSpeed(_slowMultiplier);
                }
            }
        }
    }
}
