using UnityEngine;

[CreateAssetMenu(fileName = "1_walk", menuName = "States/Movement/Walk")]
public class WalkState : State<Movement>
{
    [SerializeField] private float _moveSpeed;

    public override void Init(Movement machine, State<Movement> from)
    {
        base.Init(machine, from);
        UpdateAnimation();
    }
    public override void Exit()
    {
        ResetAnimation();
    }
    public override void ChangeState()
    { 
        if (_machine.Handler.Dodge)
            _machine.SwitchState(_machine.DodgeState);
        else if (!_machine.Handler.Move)
            _machine.SwitchState(_machine.IdleState);
    }

    public override void FixedUpdate()
    {
        var move = _machine.Handler.MoveDirection * _moveSpeed * Time.fixedDeltaTime;
        _machine.Rigidbody.MovePosition(_machine.Rigidbody.position + move);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        ResetAnimation();
        if (_machine.Handler.MoveDirection.x > 0)
            _machine.Animator.SetBool("walk_right", true);
        else if (_machine.Handler.MoveDirection.x < 0)
            _machine.Animator.SetBool("walk_left", true);
        else if (_machine.Handler.MoveDirection.y > 0)
            _machine.Animator.SetBool("walk_up", true);
        else if (_machine.Handler.MoveDirection.y < 0)
            _machine.Animator.SetBool("walk_down", true);
    }

    private void ResetAnimation()
    {
        _machine.Animator.SetBool("walk_right", false);
        _machine.Animator.SetBool("walk_left", false);
        _machine.Animator.SetBool("walk_up", false);
        _machine.Animator.SetBool("walk_down", false);
    }
}