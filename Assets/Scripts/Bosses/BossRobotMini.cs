using System.Collections;
using UnityEngine;

public class BossRobotMini : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;

    [Header("Moving")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveDelay;

    [Header("Firing")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [Space]
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletCritDamage;
    [SerializeField] private float _bulletCritChance;
    [Space]
    [SerializeField] private float _fireRate;

    private Transform _target;

    public void Awake()
    {
        StartCoroutine(Moving());
        StartCoroutine(Firing());
    }

    private void OnEnable()
    {
        _health.OnValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.OnValueChanged -= OnHealthChanged;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void RotateToTarget()
    {
        var targetPosition = (Vector2) _target.position;
        var firePosition = (Vector2) _firePoint.position;
        
        var direction = targetPosition - firePosition;

        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _firePoint.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            _animator.SetBool("move", true);
            var currentDistance = 0f;
            var direction = Random.insideUnitCircle.normalized;
            while (currentDistance < _moveDistance)
            {
                var delta = Time.fixedDeltaTime * _moveSpeed;
                _rigidbody.MovePosition(_rigidbody.position + direction * delta);
                currentDistance += delta;
                yield return new WaitForFixedUpdate();
            }
            _animator.SetBool("move", false);

            yield return new WaitForSeconds(_moveDelay);
        }
    }
    
    private IEnumerator Firing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _fireRate);
            Fire();
        }
    }

    private void Fire()
    {
        RotateToTarget();
        var fireDirection = _firePoint.right;
        var bullet = Instantiate(_bulletPrefab, _firePoint.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, fireDirection)));

        bullet.Init(
            _bulletDamage, 
            _bulletCritDamage,
            _bulletCritChance,
            fireDirection);
    }

    private void OnHealthChanged(float health)
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}