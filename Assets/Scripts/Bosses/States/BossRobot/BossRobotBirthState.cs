using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Robot/Birth")]
public class BossRobotBirthState : State<Boss>
{
    [SerializeField] private BossRobotMini _childPrefab;

    private BossRobot _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossRobot) machine;
        var mini = Instantiate(_childPrefab, _boss.BirthPoint.position, Quaternion.identity);
        mini.SetTarget(_boss.Target);
    }

    public override void ChangeState()
    {
        _boss.SwitchState(_boss.IdleState);
    }
}