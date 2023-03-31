using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class HomingMissle : Projectile
{
    [SerializeField] protected Rigidbody2D _rigidbody;

    [SerializeField] private AnimationCurve _bulletSpeedCurve;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _findTarget;
    [SerializeField] private float _findTargetDistance = 20;

    private float _time = 0;
    private Transform _target;
    private float _additiveSpeed;

    public override void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        base.Init(damage, critDamage, critChance, direction);
        _rigidbody.velocity = _fireDirection * (_bulletSpeedCurve.Evaluate(_time) + _additiveSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_findTarget)
        {
            SetTarget(FindClosestTarget());
        }
        if (_target == null) return;

        UpdateRotation();
        _rigidbody.velocity = transform.right * (_bulletSpeedCurve.Evaluate(_time) + _additiveSpeed);
    }

    private void UpdateRotation()
    {
        var direction = _rigidbody.velocity.normalized;
        var targetDirection = (_target.position - transform.position).normalized;

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float targetRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        var currentRotation = Mathf.MoveTowardsAngle(rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        
        if (Mathf.Abs(currentRotation) > 90f)
            transform.rotation = Quaternion.Euler(180f, 0f, -currentRotation);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
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
        Destroy(gameObject);
    }

    private Transform FindClosestTarget()
    {
        var targets = FindObjectsOfType<Health>();
        var distance = Mathf.Infinity;
        Transform target = null;

        foreach (var health in targets)
        {
            if (health.Type != _damageableHealthType) continue;
            
            var currentDistance = Vector2.Distance(health.transform.position, transform.position);
            if (currentDistance > _findTargetDistance) continue;

            if (currentDistance < distance)
            {
                target = health.transform;
                distance = currentDistance;
            }
        }

        return target;
    }

    public override void IncreaseSpeed(float value, float percent)
    { 
        _additiveSpeed += value + _bulletSpeedCurve.Evaluate(0) * percent;
    }
}