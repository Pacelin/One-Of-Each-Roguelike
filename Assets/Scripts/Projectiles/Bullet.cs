using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float _bulletSpeed;

    protected override void Init()
    {
        _rigidbody.velocity = _direction.normalized * _bulletSpeed;
    }
    protected override void Hit(Health health)
    {
        var critChance = _weapon.WeaponValues.GetCritChance(_weapon.Upgrades);
        var damageMultiplier = 1f;
        if (critChance <= Random.Range(0f, 1f))
            damageMultiplier = _weapon.WeaponValues.GetCritDamageMultiplier(_weapon.Upgrades) + (critChance > 1 ? (critChance - 1) : 0);

        health.TakeDamage(_weapon.WeaponValues.GetDamage(_weapon.Upgrades) * damageMultiplier);
        Destroy(gameObject);
    }
}