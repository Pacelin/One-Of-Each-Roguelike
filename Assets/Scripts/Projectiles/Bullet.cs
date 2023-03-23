using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : Projectile
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Collider2D _collider;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private int _penetrationCount = 0;
    [SerializeField] private int _bouncesCount = 0;

    public override void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        base.Init(damage, critDamage, critChance, direction);
        _rigidbody.velocity = _fireDirection * _bulletSpeed;
        _rigidbody.angularVelocity = _rigidbody.velocity.x < 0 ? -_rotationSpeed : _rotationSpeed;
    }
    
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (_bouncesCount > 0)
            {
                _bouncesCount--;
                Reflect(collision);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            var health = collision.gameObject.GetComponent<Health>();
            if (health == null || health.Type != _damageableHealthType) return;

            Hit(health);

            if (_bouncesCount > 0)
            {
                _bouncesCount--;
                Reflect(collision);
            }
            else if (_penetrationCount > 0)
            {
                _penetrationCount--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }*/

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
        Destroy(gameObject);
    }

    private void Reflect(Collision2D collision)
    {
        var newDirection = Vector2.Reflect(_rigidbody.velocity, collision.contacts[0].normal).normalized;
        _rigidbody.velocity = newDirection * _bulletSpeed;
        transform.right = _rigidbody.velocity;
    }

}