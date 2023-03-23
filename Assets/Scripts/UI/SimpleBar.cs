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
        if (_notificator != null && !_notificator.TryGetComponent<IBarNotificator>(out _))
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

    private void Update()
    {
        if (Notificator != null)
        {
            if (Notificator.GetMin() != Range.x || Notificator.GetMax() != Range.y)
                SetRange(Notificator.GetMin(), Notificator.GetMax());
            
            if (Notificator.GetCurrent() != Value)
                SetValue(Notificator.GetCurrent());   
        }  
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
