using UnityEngine;

[CreateAssetMenu(fileName = "0_bar_idle", menuName = "States/UI/Bar/Idle")]
public class BarIdleState : State<Bar>
{
    public override void ChangeState()
    { 
        if (_machine.Value < _machine.ActualBarValue)
            _machine.SwitchState(_machine.DecreaseState);
        else if (_machine.Value > _machine.ActualBarValue)
            _machine.SwitchState(_machine.IncreaseState);
    }
}