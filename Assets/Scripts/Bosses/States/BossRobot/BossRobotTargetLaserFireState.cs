using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Robot/Target Laser Fire")]
public class BossRobotTargetLaserFireState : State<Boss>
{
    [SerializeField] private float _angleOffset;
    [SerializeField] private float _laserSpeed;
    [SerializeField] private float _fireTime;

    private BossRobot _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossRobot) machine;

        _boss.Animator.SetBool("laser_firing", true);
        _boss.StartCoroutine(Firing());
    }

    public override void Exit()
    {
        _boss.Animator.SetBool("laser_firing", false);
        _boss.Laser.DisableLaser();
    }

    private IEnumerator Firing()
    {
        var angle = _boss.GetTargetLaserAngle() +
            (Random.Range(0, 2) == 0 ? -_angleOffset : _angleOffset);
        
        _boss.Laser.SetStartPosition(_boss.LaserStart.position);
        _boss.SetLaserRotation(angle);
        _boss.Laser.EnableLaser();

        for (float t = 0; t < _fireTime; t += Time.deltaTime)
        {
            _boss.Laser.SetStartPosition(_boss.LaserStart.position);
            _boss.SetLaserRotation(angle);
            _boss.Laser.ApplyDamage();
            
            angle = Mathf.MoveTowardsAngle(angle, _boss.GetTargetLaserAngle(), _laserSpeed * Time.deltaTime);
            yield return null;
        }

        _machine.SwitchState(_boss.IdleState);
    }
}