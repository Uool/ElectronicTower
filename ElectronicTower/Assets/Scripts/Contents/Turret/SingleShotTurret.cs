using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTurret : Turret
{
    public override void Init()
    {
        base.Init();
    }

    protected override void Shoot()
    {
        Projectile projectile = Managers.Resource.Instantiate(turretData.projectile, firePoint).GetComponent<Projectile>();
        projectile.Init(gameObject, target, firePoint.position, turretData.Damage);
        // TODO : »ç¿îµå
    }

    // Update is called once per frame
    void Update()
    {
        TurretUpdate();
    }
}
