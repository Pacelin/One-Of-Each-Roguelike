using System;
using System.Collections;
using UnityEngine;

public class HealthArray : MonoBehaviour, IBarNotificator
{
    [SerializeField] private Health[] _healths;

    private float _max;

    private void Awake()
    {
        _max = 0;

        foreach (var health in _healths)
            _max += health.GetMax();
    }

    public void SetCanDamaged(bool canDamaged)
    {
        foreach (var health in _healths)
            health?.SetCanDamaged(canDamaged);
    }

    public virtual float GetMin() => 0;
    public virtual float GetMax() => _max;
    public virtual float GetCurrent()
    {
        var current = 0f;
        
        foreach (var health in _healths)
            if (health != null)
                current += health.GetCurrent();

        return current;
    }
}