using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "2_dodge", menuName = "States/Movement/Dodge")]
public class DodgeState : State<Movement>
{
    [SerializeField] private float _dodgeSpeed;
    [SerializeField] private float _dodgeDistance;
    [SerializeField] private float _dodgeCooldown;

    private Vector2 _dodgeDirection;

    private float _distance = 0f;
    private bool _canDodge = true;

    public override void Init(Movement machine, State<Movement> from) 
    {
        base.Init(machine, from);

        if (!_canDodge)
        {
            SwitchState();
        }
        else
        {
            _distance = 0f;
            _canDodge = false;
            _dodgeDirection = _machine.Handler.DodgeDirection;
            _machine.Animator.SetTrigger("dodge");
        }
    }

    public override void ChangeState()
    {
        if (_distance >= _dodgeDistance)
        {    
            _machine.StartCoroutine(CooldownTimer());
            SwitchState();
        }
    }

    public override void FixedUpdate()
    {
        var span = _dodgeSpeed * Time.fixedDeltaTime;
        _machine.Rigidbody.MovePosition(_machine.Rigidbody.position + _dodgeDirection * span);
        _distance += span;
    }

    private IEnumerator CooldownTimer() 
    {
        yield return new WaitForSeconds(_dodgeCooldown);
        _canDodge = true;
    }

    private void SwitchState()
    {
        if (_machine.Handler.Move)
            _machine.SwitchState(_machine.WalkState);
        else
            _machine.SwitchState(_machine.IdleState);
    }
}