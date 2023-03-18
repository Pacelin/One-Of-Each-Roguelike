using UnityEngine;

[CreateAssetMenu(fileName = "0_idle", menuName = "States/Movement/Idle")]
public class IdleState : State<Movement>
{
    public override void Init(Movement machine)
    {
        base.Init(machine);
        _machine.Animator.SetBool("idle", true);
    }

    public override void Exit()
    {
        _machine.Animator.SetBool("idle", false);
    }
    
    public override void ChangeState()
    { 
        if (_machine.Handler.Dodge)
            _machine.SwitchState(_machine.DodgeState);
        else if (_machine.Handler.Move)
            _machine.SwitchState(_machine.WalkState);
    }
}