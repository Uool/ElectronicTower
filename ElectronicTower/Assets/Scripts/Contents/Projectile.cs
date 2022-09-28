using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class Projectile : MonoBehaviour
{
    private GameObject _owner;
    private Transform _target;
    private Vector3 _originPos;
    private float _damage;
    private float _speed = 30f;
    private Define.ETurretType _ownerType;

    public GameObject impactEffect;
    public float explosionRadius;
    

    public void OnEnable()
    {
        transform.position = _originPos;
    }

    public void Init(GameObject owner, Transform target, Vector3 originPos, float damage)
    {
        _owner = owner;
        _target = target;
        _originPos = originPos;
        _damage = damage;

        _ownerType = _owner.GetComponent<Turret>().turretData.Type;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;

        float distanceThisFrame = _speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Effect();

        if (explosionRadius > 0f)
        {
            Explosion();
        }
        else
        {
            Health targetHealth = _target.GetComponent<Health>();
            targetHealth.TakeDamage(_damage, _owner);
        }

        Managers.Resource.Destroy(gameObject);
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider target in colliders)
        {
            if (target.tag == "Enemy")
            {
                Health targetHealth = target.GetComponent<Health>();
                targetHealth.TakeDamage(_damage, _owner);
            }
        }
    }

    void Effect()
    {
        GameObject effect;
        float destroyDelay = 0f;
        switch (_ownerType)
        {
            case Define.ETurretType.MachineGun:
                effect = Managers.Resource.Instantiate("Effect/BulletHit");
                destroyDelay = 1f;
                break;
            case Define.ETurretType.Rocket:
                effect = Managers.Resource.Instantiate("Effect/RocketExplosionPfx");
                destroyDelay = 2.5f;
                break;
            default:
                effect = null;
                break;
        }
        if (null != effect)
        {
            effect.transform.position = transform.position;
            Managers.Resource.Destroy(effect, destroyDelay);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
