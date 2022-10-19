using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTurret : Turret
{
    private int _fireCount;
    private int _fireMaxCount;
    public override void Init()
    {
        base.Init();
        _fireMaxCount = firePoints.Length;
    }

    protected override void Shoot()
    {
        if (firePoints.Length < 2)
        {
            Projectile projectile = Managers.Resource.Instantiate(turretData.projectile, firePoints[0]).GetComponent<Projectile>();
            projectile.Init(gameObject, target, firePoints[0].position, turretData.Damage);
        }
        else
        {
            _fireCount = (_fireCount <= _fireMaxCount) ? 0 : _fireCount;
            Projectile projectile = Managers.Resource.Instantiate(turretData.projectile, firePoints[_fireCount]).GetComponent<Projectile>();
            projectile.Init(gameObject, target, firePoints[_fireCount].position, turretData.Damage);
        }

        PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        TurretUpdate();
    }

    void PlaySound()
    {
        switch (turretData.Type)
        {
            case Define.ETurretType.MachineGun:
                Managers.Sound.Play("Turret/MachineGunShoot");
                break;
            case Define.ETurretType.Rocket:
                Managers.Sound.Play("Turret/RocketShoot");
                break;
        }
    }
}
