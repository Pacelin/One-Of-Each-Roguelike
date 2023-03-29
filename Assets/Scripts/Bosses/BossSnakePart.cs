using UnityEngine;

public class BossSnakePart : Boss
{
    [Header("Parts")]
    public BossSnakePart NextPart;
    public BossSnakePart PreviousPart;
    
    [Header("Fire Points")]
    public Transform HeadFirePoint;
    public Transform BodyFirePoint1;
    public Transform BofyFirePoint2;

    [Header("Settings")]
    public Sprite HeadSprite;

    [Header("States")]
    public State<Boss> HeadIdleState;
    public State<Boss> BodyPartIdleState;
    public State<Boss> HeadHomingFireState;
    public State<Boss> HeadHomingExtraFireState;
    public State<Boss> SingleHeadFireState;
    public State<Boss> BodyPartFireState;


    private void Awake()
    {
        HeadIdleState = Instantiate(HeadIdleState);
        BodyPartIdleState = Instantiate(BodyPartIdleState);
        HeadHomingFireState = Instantiate(HeadHomingFireState);
        HeadHomingExtraFireState = Instantiate(HeadHomingExtraFireState);
        BodyPartFireState = Instantiate(BodyPartFireState);
        SingleHeadFireState = Instantiate(SingleHeadFireState);
    }
    private void OnDrawGizmos()
    {
        if (_currentState is BossSnakeHeadState state)
            state.OnDrawGizmos();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (NextPart != null)
            NextPart.OnDeath += OnNextPartDeath;
        if (PreviousPart != null)
            PreviousPart.OnDeath += OnPreviousPartDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (NextPart != null)
            NextPart.OnDeath -= OnNextPartDeath;
        if (PreviousPart != null)
            PreviousPart.OnDeath -= OnPreviousPartDeath;
    }

    public override void Activate()
    {
        if (NextPart == null) 
            SwitchState(HeadIdleState);
        else 
            SwitchState(BodyPartIdleState);
    }

    public void SetRotation(float rotationZ)
    {
        if (Mathf.Abs(rotationZ) > 90f)
            transform.rotation = Quaternion.Euler(180f, 0f, -rotationZ);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void SetRotationByDirection(Transform target)
    {
        var targetPosition = (Vector2) target.position;
        var myPosition = (Vector2) transform.position;
        
        var direction = targetPosition - myPosition;

        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SetRotation(rotationZ);
    }

    private void OnNextPartDeath()
    {
        NextPart = null;
        if (PreviousPart == null)
            SwitchState(SingleHeadFireState);
        else
            SwitchState(HeadIdleState);
    }

    private void OnPreviousPartDeath()
    {
        PreviousPart = null;
        if (NextPart == null)
            SwitchState(SingleHeadFireState);
    }
}
