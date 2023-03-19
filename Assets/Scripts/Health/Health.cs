using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Health : MonoBehaviour, IBarNotificator
{
    public event Action<float> OnValueChanged;

    public float Max => _max;
    public float Current => _current;

    public HealthType Type;

    [SerializeField] private float _max;
    [SerializeField] private float _current;

    private float _healAmount;

    private void OnValidate()
    {
        _current = Mathf.Clamp(_current, 0, _max);
    }

    public void TakeDamage(float amount) => AddHealth(-amount);
    public void Heal(float amount) => AddHealth(amount);
    public void Heal(float amount, float rate) => StartCoroutine(Healing(amount, rate));

    private IEnumerator Healing(float amount, float rate)
    {
        _healAmount += amount;
        if (_healAmount == amount) yield break;
        
        while (_healAmount > 0)
        {
            var healed = Mathf.Min(rate, _healAmount);
            AddHealth(healed);
            _healAmount -= healed;

            if (_current == _max)
            {
                _healAmount = 0;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void AddHealth(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0, _max);
        OnValueChanged?.Invoke(_current);
    }

    public float GetMin() => 0;
    public float GetMax() => _max;
    public float GetCurrent() => _current;
}