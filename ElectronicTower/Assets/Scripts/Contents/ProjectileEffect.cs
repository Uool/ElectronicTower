using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private GameObject _owner;
    private Define.ETurretType _type;

    private float _damage;
    private float _explosionRad = 20f;
    private string _enemyTag = "Enemy";

    public void Init(GameObject owner, float damage, Define.ETurretType type)
    {
        _owner = owner;
        _damage = damage;
        _type = type;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(_enemyTag))
        {
            if (other.GetComponent<Health>())
            {
                if (_type == Define.ETurretType.Laser)
                    other.GetComponent<Health>().TakeDamage(_damage * Time.deltaTime, other);
                else if (_type == Define.ETurretType.Rocket)
                    Explosion();
                else
                    other.GetComponent<Health>().TakeDamage(_damage, other);
            }
        }
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRad);
        foreach (Collider target in colliders)
        {
            if (target.tag == "Enemy")
            {
                Health targetHealth = target.GetComponent<Health>();
                targetHealth.TakeDamage(_damage, _owner);
            }
        }
    }
}
