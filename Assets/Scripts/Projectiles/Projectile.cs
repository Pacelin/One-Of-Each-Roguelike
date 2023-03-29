using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected HealthType _damageableHealthType;
    
    public float Damage => _damage;
    protected float _damage;
    
    public float CritDamage => _critDamage;
    protected float _critDamage;

    public float CritChance => _critChance;
    protected float _critChance;
    
    protected Vector2 _fireDirection;

    public virtual void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        _damage = damage;
        _critDamage = critDamage;
        _critChance = critChance;
        _fireDirection = direction.normalized;
    }

    public virtual void SetDamage(float damage) => 
        _damage = damage;
    public virtual void SetCritChance(float chance) => 
        _critChance = chance;
    public virtual void SetCritDamage(float damage) => 
        _critDamage = damage;

    public virtual void SetDirection(Vector2 direction) =>
        _fireDirection = direction.normalized;

    protected virtual void Hit(Health health)
    {
        if (Random.Range(0f, 1f) < _critChance)
            health.TakeDamage(_critDamage);
        else 
            health.TakeDamage(_damage);
    }
}