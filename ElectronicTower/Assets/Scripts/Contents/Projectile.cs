using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _owner;
    private Transform _target;
    private float _damage;

    public GameObject impactEffect;
    public float explosionRadius;
    public float speed = 70f;

    public void Init(GameObject owner, Transform target, float damage)
    {
        _owner = owner;
        _target = target;
        _damage = damage;
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
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // TODO : 이팩트 생성
        //Managers.Resource.Instantiate();

        
        //TODO : 타겟 체력 줄어들기.
        //targetHealth.TakeDamage(_damage, _owner);

        if (explosionRadius > 0f)
        {
            Explosion();
        }
        else
        {
            Health targetHealth = _target.GetComponent<Health>();

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
                //target.TakeDamage(_damage, _owner);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
