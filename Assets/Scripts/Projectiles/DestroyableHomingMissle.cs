using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class DestroyableHomingMissle : HomingMissle
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