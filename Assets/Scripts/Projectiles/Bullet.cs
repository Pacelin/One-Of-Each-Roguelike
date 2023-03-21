using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _rotationSpeed;

    protected override void Init()
    {
        _rigidbody.velocity = _direction.normalized * _bulletSpeed;
        _rigidbody.angularVelocity = _rigidbody.velocity.x < 0 ? -_rotationSpeed : _rotationSpeed;
    }
    protected override void Hit(Health health)
    {
        var critChance = _weapon.Data.CritChance;
        var damageMultiplier = 1f;
        if (critChance > Random.Range(0f, 1f))
            damageMultiplier = _weapon.Data.CritDamageMultiplier + (critChance > 1 ? (critChance - 1) : 0);

        health.TakeDamage(_weapon.Data.Damage * damageMultiplier);
        Destroy(gameObject);
    }
}