using UnityEngine;
using UnityEngine.UI;

public class Bar : StateMachine<Bar>
{
    [Header("References")]
    public Image ActualBar;
    public Image IncreasingBar;
    public Image DecreasingBar;

    [Header("Settings")]
    public Vector2 Range;
    [SerializeField] private float _initialValue;
    [SerializeField] private bool _animateOnAwake;

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
        _initialValue = Mathf.Clamp(_initialValue, Range.x, Range.y);
        //ActualBar.fillAmount = (_initialValue - Range.x) / RangeLength;
        SetValue(_initialValue);
    }

    protected override void Start()
    {
        Value = _initialValue;

        IncreasingBar.enabled = false;
        DecreasingBar.enabled = false;
        ActualBarValue = _animateOnAwake ? Range.x : _initialValue;

        _currentState = IdleState;
        _currentState.Init(this);
    }

    public void SetValue(float value) =>
        Value = Mathf.Clamp(value, Range.x, Range.y);

    public void SetRange(float low, float high) 
    {
        Range = new Vector2(low, high);
        SetValue(Value);
    }
}