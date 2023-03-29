using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Head Homing Missle Fire")]
public class BossSnakeHeadHomingFireState : BossSnakeHeadState
{
    [SerializeField] private HomingMissle _homingMisslePrefab;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        var missle = Instantiate(_homingMisslePrefab, _boss.HeadFirePoint.position, Quaternion.identity);
        missle.Init(1, 0, 0, _boss.HeadFirePoint.right);
        missle.SetTarget(_boss.Target);
    }

    public override void ChangeState()
    {
        _boss.SwitchState(_boss.HeadIdleState);
    }
}
