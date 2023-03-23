using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected HealthType _damageableHealthType;
    
    protected float _damage;
    protected float _critDamage;
    protected float _critChance;
    protected Vector2 _fireDirection;

    public virtual void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        _damage = damage;
        _critDamage = critDamage;
        _critChance = critChance;
        _fireDirection = direction.normalized;
    }

    public virtual void SetDirection(Vector2 direction) =>
        _fireDirection = direction.normalized;

    protected virtual void Hit(Health health)
    {
        if (Random.Range(0, 1) < _critChance)
            health.TakeDamage(_critDamage);
        else 
            health.TakeDamage(_damage);
    }
}