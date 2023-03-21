using UnityEngine;

public class Movement : StateMachine<Movement>
{
    [Header("References")]
    public Rigidbody2D Rigidbody;
    public MovementHandler Handler;
    public Animator Animator;

    [Header("States")]
    public State<Movement> IdleState;
    public State<Movement> WalkState;
    public State<Movement> DodgeState;

    protected override void Start()
    {
        _currentState = IdleState;
        _currentState.Init(this, null);
    }

    protected override void Update()
    {
        Handler.UpdateHandler();
        base.Update();
    }
}