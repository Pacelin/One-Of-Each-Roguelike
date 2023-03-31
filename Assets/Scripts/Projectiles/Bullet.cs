using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : Projectile
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Collider2D _collider;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private int _penetrationCount = 0;

    public override void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        base.Init(damage, critDamage, critChance, direction);
        _rigidbody.velocity = _fireDirection * _bulletSpeed;
        _rigidbody.angularVelocity = _rigidbody.velocity.x < 0 ? -_rotationSpeed : _rotationSpeed;
    }
    public override void IncreaseSpeed(float value, float percent)
    { 
        _rigidbody.velocity = _rigidbody.velocity.normalized * (_rigidbody.velocity.magnitude + value + _rigidbody.velocity.magnitude * percent);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            Destroy(gameObject);
            return;
        }

        var health = other.GetComponent<Health>();
        if (health == null || health.Type != _damageableHealthType) return;

        Hit(health);
        if (_penetrationCount-- <= 0)
            Destroy(gameObject);
    }
}