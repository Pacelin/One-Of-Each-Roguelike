using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Robot/Aroound Laser Fire")]
public class BossRobotAroundLaserFireState : State<Boss>
{
    [SerializeField] private float _angleStart;
    [SerializeField] private float _angleEnd;
    [SerializeField] private float _laserSpeed;

    private BossRobot _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossRobot) machine;

        _boss.StartCoroutine(Firing());
        _boss.Animator.SetBool("laser_firing", true);
    }

    public override void Exit()
    {
        _boss.Animator.SetBool("laser_firing", false);
        _boss.Laser.DisableLaser();
    }

    private IEnumerator Firing()
    {
        var angle = _angleStart;

        _boss.Laser.SetStartPosition(_boss.LaserStart.position);
        _boss.SetLaserRotation(90 + angle);
        _boss.Laser.EnableLaser();
        while (angle < _angleEnd)
        {
            _boss.Laser.SetStartPosition(_boss.LaserStart.position);
            _boss.SetLaserRotation(90 + angle);
            _boss.Laser.ApplyDamage();

            angle += Time.deltaTime * _laserSpeed;
            yield return null;
        }

        _machine.SwitchState(_boss.IdleState);
    }
}