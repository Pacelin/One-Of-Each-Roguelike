using UnityEngine;
using UnityEngine.UI;

public class Bar : StateMachine<Bar>
{    
    public IBarNotificator Notificator;

    [Header("References")]
    public Image ActualBar;
    public Image IncreasingBar;
    public Image DecreasingBar;
    [SerializeField] private GameObject _notificator;

    [Header("Settings")]
    public Vector2 Range;
    [SerializeField] private float _initialValue;

    [Header("States")]
    public State<Bar> IdleState;
    public State<Bar> IncreaseState;
    public State<Bar> DecreaseState;

    public float Value { get; private set; }
    public float RangeLength => Range.y - Range.x;

    public float ActualBarValue 
    { 
        get => _actualBarValue; 
        set
        {
            var correctValue = Mathf.Clamp(value, Range.x, Range.y);
            _actualBarValue = correctValue;
            ActualBar.fillAmount = (correctValue - Range.x) / RangeLength;
        }
    }
    public float IncreasingBarValue 
    { 
        get => _increasingBarValue; 
        set
        {
            var correctValue = Mathf.Clamp(value, Range.x, Range.y);
            _increasingBarValue = correctValue;
            IncreasingBar.fillAmount = (correctValue - Range.x) / RangeLength;
        }
    }
    public float DecreasingBarValue 
    { 
        get => _decreasingBarValue; 
        set
        {
            var correctValue = Mathf.Clamp(value, Range.x, Range.y);
            _decreasingBarValue = correctValue;
            DecreasingBar.fillAmount = (correctValue - Range.x) / RangeLength;
        }
    }

    private float _actualBarValue;
    private float _increasingBarValue;
    private float _decreasingBarValue;

    private void OnValidate()
    {
        if (_notificator != null && !_notificator.TryGetComponent<IBarNotificator>(out _))
            _notificator = null;
    }

    private void Awake()
    {
        IncreasingBar.enabled = false;
        DecreasingBar.enabled = false;
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

    protected override void Start()
    {
        _currentState = IdleState;
        _currentState.Init(this, null);
    }

    protected override void Update()
    {
        if (Notificator != null)
        {
            if (Notificator.GetMin() != Range.x || Notificator.GetMax() != Range.y)
                SetRange(Notificator.GetMin(), Notificator.GetMax());
            
            if (Notificator.GetCurrent() != Value)
                SetValue(Notificator.GetCurrent());   
        }
        base.Update();
    }

    public void SetValue(float value) =>
        Value = Mathf.Clamp(value, Range.x, Range.y);

    public void SetRange(float low, float high) 
    {
        Range = new Vector2(low, high);
        SetValue(Value);
    }
}