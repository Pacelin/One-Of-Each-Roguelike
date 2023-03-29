using System.Diagnostics;
using UnityEngine;

public abstract class DestroyableProjectile : Projectile
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.OnValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.OnValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}