using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBar : MonoBehaviour
{
    public IBarNotificator Notificator;
    
    [Header("References")]
    public Image Bar;
    [SerializeField] private GameObject _notificator;

    [Header("Settings")]
    public Vector2 Range;
    [SerializeField] private float _initialValue;

    public float Value { get; private set; }
    public float RangeLength => Range.y - Range.x;

    private void OnValidate()
    {
        _initialValue = Mathf.Clamp(_initialValue, Range.x, Range.y);
        SetValue(_initialValue);
        if (!_notificator.TryGetComponent<IBarNotificator>(out _))
            _notificator = null;
    }

    private void Awake()
    {
        if (_notificator != null)
        {
            Notificator = _notificator.GetComponent<IBarNotificator>();
            if (Notificator != null) 
            {
                SetRange(Notificator.GetMin(), Notificator.GetMax());
                SetValue(Notificator.GetCurrent());   
            }
            else
            {
                SetValue(_initialValue);
            }
        }
    }

    private void OnEnable()
    {
        if (Notificator != null)
            Notificator.OnValueChanged += SetValue;
    }

    private void OnDisable()
    {
        if (Notificator != null)
            Notificator.OnValueChanged -= SetValue;
    }

    public void SetValue(float value)
    { 
        Value = Mathf.Clamp(value, Range.x, Range.y);
        Bar.fillAmount = (Value - Range.x) / RangeLength;
    }

    public void SetRange(float low, float high) 
    {
        Range = new Vector2(low, high);
        SetValue(Value);
    }
}
