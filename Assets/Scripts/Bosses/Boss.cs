using UnityEngine;
using System;
using System.Collections;

public abstract class Boss : StateMachine<Boss>
{
    public event Action OnDeath;

    public Health Health;
    public Rigidbody2D Rigidbody2D;
    public SpriteRenderer BossSpriteRenderer;
    public Collider2D HitBox;
    public Transform Target;
    [Space]
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _poisonColor;
    [SerializeField] private Trigger _activationTrigger;

    [Header("States")]
    public State<Boss> PrepareState;

    protected Coroutine _colorChangeCoroutine;

    protected override void Start()
    {
        _currentState = PrepareState;
        PrepareState.Init(this, null);
    }

    private void OnEnable()
    {
        Health.OnValueChanged += OnHealthChanged;
        Health.OnDamaged += OnDamaged;
        Health.OnHeal += OnHeal;
        _activationTrigger.OnTrigger += Activate;
    }

    private void OnDisable()
    {
        Health.OnValueChanged -= OnHealthChanged;
        Health.OnDamaged -= OnDamaged;
        Health.OnHeal -= OnHeal;
        _activationTrigger.OnTrigger += Activate;
    }

    public abstract void Activate();
    public virtual void KillSelf()
    {
        SwitchState(PrepareState);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void OnHealthChanged(float health)
    {
        if (health <= 0) KillSelf();
    }

    protected virtual void OnDamaged()
    {
        if (_colorChangeCoroutine != null)
            StopCoroutine(_colorChangeCoroutine);
        _colorChangeCoroutine = StartCoroutine(ColorChanging(_damageColor, 0.5f));
    }
    protected virtual void OnHeal()
    {
        if (_colorChangeCoroutine != null)
            StopCoroutine(_colorChangeCoroutine);
        _colorChangeCoroutine = StartCoroutine(ColorChanging(_healColor, 0.5f));
    }

    protected IEnumerator ColorChanging(Color color, float time)
    {
        var defaultColor = Health.Poisoned ? _poisonColor : Color.white;
        
        BossSpriteRenderer.color = color;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            BossSpriteRenderer.color = Color.Lerp(color, defaultColor, t / time);
            yield return null;
        }

        BossSpriteRenderer.color = defaultColor;
    }
}
