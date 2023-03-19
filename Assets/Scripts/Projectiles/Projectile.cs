using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected HealthType _damageableHealthType;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _lifeTime;

    protected float _timeElapsed;
    protected Weapon _weapon;
    protected Vector2 _direction;

    public void Init(Weapon weapon, Vector2 direction)
    {
        _weapon = weapon;
        _direction = direction;
        Init();
    }
    protected abstract void Init();
    protected abstract void Hit(Health health);
    protected virtual void HitWall()
    {
        RemoveProjectile();
    }

    protected virtual void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed > _lifeTime)
            RemoveProjectile();
    }

    protected virtual void RemoveProjectile()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            HitWall();
            return;
        }

        var health = other.GetComponent<Health>();
        if (health == null || health.Type != _damageableHealthType) return;

        Hit(health);
    }
}