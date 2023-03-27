using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Health : MonoBehaviour, IBarNotificator
{
    public event Action<float> OnValueChanged;
    public event Action OnDamaged;
    public event Action OnHeal;

    public float Max => _max;
    public float Current => _current;
    public bool Poisoned => _currentPoisonTime > 0 && !_poisonImmune;

    public HealthType Type;

    [SerializeField] private float _max;
    [SerializeField] private float _current;
    [SerializeField] private bool _poisonImmune;

    private float _healAmount;
    private float _poisonDamage;

    private float _currentPoisonTime;
    private float _timer;
    private bool _canDamaged = true;

    public void SetPoison(float poisonTime, float poisonDamage)
    {
        _currentPoisonTime = poisonTime;
        _poisonDamage = poisonDamage;
    }

    public void SetCanDamaged(bool canDamaged) => _canDamaged = canDamaged;
    private void Update()
    {
        if (_poisonImmune || !_canDamaged || _currentPoisonTime <= 0) return;

        _currentPoisonTime -= Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            TakeDamage(_poisonDamage);
            _timer = 0;
        }
    }

    public void TakeDamage(float amount)
    {
        if (!_canDamaged) return;

        AddHealth(-amount);
        OnDamaged?.Invoke();
    }
    public void Heal(float amount) 
    {
        AddHealth(amount);
        OnHeal?.Invoke();
    }

    public void Heal(float amount, float rate) => StartCoroutine(Healing(amount, rate));

    private IEnumerator Healing(float amount, float rate)
    {
        _healAmount += amount;
        if (_healAmount == amount) yield break;
        
        while (_healAmount > 0)
        {
            var healed = Mathf.Min(rate, _healAmount);
            AddHealth(healed);
            OnHeal?.Invoke();
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