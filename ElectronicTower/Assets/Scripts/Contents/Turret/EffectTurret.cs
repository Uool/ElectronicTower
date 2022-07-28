using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTurret : Turret
{
    private ParticleSystem _projectile;

    public override void Init()
    {
        base.Init();
        _projectile = Managers.Resource.Instantiate(turretData.projectile.gameObject, firePoints[0]).GetComponent<ParticleSystem>();
        _projectile.GetComponent<ProjectileEffect>().Init(gameObject, turretData.Damage, turretData.SlowMultiplier, turretData.Type);
    }

    protected override void Shoot()
    {
        if (_projectile.isPlaying == false)
            _projectile.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (_projectile.isPlaying)
                _projectile.Stop();
            return;
        }

        TurretUpdate();
    }
}
