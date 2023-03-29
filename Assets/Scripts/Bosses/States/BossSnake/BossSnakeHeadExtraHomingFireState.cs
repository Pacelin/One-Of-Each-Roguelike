using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Head Extra Homing Missle Fire")]
public class BossSnakeHeadExtraHomingFireState : BossSnakeHeadState
{
    [SerializeField] private HomingMissle _homingMisslePrefab;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        var missle1 = Instantiate(_homingMisslePrefab, _boss.HeadFirePoint.position, Quaternion.identity);
        missle1.Init(1, 0, 0, _boss.HeadFirePoint.right);
        missle1.SetTarget(_boss.Target);
        var missle2 = Instantiate(_homingMisslePrefab, _boss.HeadFirePoint.position, Quaternion.identity);
        missle2.Init(1, 0, 0, Vector2.Lerp(_boss.HeadFirePoint.right, _boss.HeadFirePoint.up, 0.5f));
        missle2.SetTarget(_boss.Target);
        var missle3 = Instantiate(_homingMisslePrefab, _boss.HeadFirePoint.position, Quaternion.identity);
        missle3.Init(1, 0, 0, Vector2.Lerp(_boss.HeadFirePoint.right, -_boss.HeadFirePoint.up, 0.5f));
        missle3.SetTarget(_boss.Target);
    }

    public override void ChangeState()
    {
        _boss.SwitchState(_boss.HeadIdleState);
    }
}
