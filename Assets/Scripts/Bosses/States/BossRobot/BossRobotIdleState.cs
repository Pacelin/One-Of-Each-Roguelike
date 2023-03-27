using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Robot/Idle")]
public class BossRobotIdleState : State<Boss>
{
    [SerializeField] private float _idleTime;
    
    [Header("Chances")]
    [SerializeField] private float _moveChanceMin;
    [SerializeField] private float _moveChanceDelta;
    [Space]
    [SerializeField] private float _birthChanceMin;
    [SerializeField] private float _birthChanceDelta;
    [Space]
    [SerializeField] private float _laserAroundFireChanceMin;
    [SerializeField] private float _laserAroundFireChanceDelta;
    [Space]
    [SerializeField] private float _laserTargetFireChanceMin;
    [SerializeField] private float _laserTargetFireChanceDelta;

    private BossRobot _boss;
    private float _moveChance;
    private float _birthChance;
    private float _laserAroundFireChance;
    private float _laserTargetFireChance;
    private bool _firstInit;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossRobot) machine;
        if (!_firstInit)
        {
            _moveChance = _moveChanceMin;
            _birthChance = _birthChanceMin;
            _laserAroundFireChance = _laserAroundFireChanceMin;
            _laserTargetFireChance = _laserTargetFireChanceMin;
            _firstInit = true;
        }
        _boss.StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_idleTime);

        var random = Random.Range(0, _moveChance + _birthChance + _laserAroundFireChance + _laserTargetFireChance);

        if (random < _moveChance)
        {
            _moveChance = _moveChanceMin;
            _laserAroundFireChance += _laserAroundFireChanceDelta;
            _laserTargetFireChance += _laserTargetFireChanceDelta;
            _birthChance += _birthChanceDelta;
            _boss.SwitchState(_boss.MovementState);
        }
        else if (random < _moveChance + _birthChance)
        {
            _birthChance = _birthChanceMin;
            _moveChance += _moveChanceDelta;
            _laserAroundFireChance += _laserAroundFireChanceDelta;
            _laserTargetFireChance += _laserTargetFireChanceDelta;
            _boss.SwitchState(_boss.BirthState);
        }
        else if (random < _moveChance + _birthChance + _laserAroundFireChance)
        {
            _laserAroundFireChance = _laserAroundFireChanceMin;
            _moveChance += _moveChanceDelta;
            _birthChance += _birthChanceDelta;
            _laserTargetFireChance += _laserTargetFireChanceDelta;
            _boss.SwitchState(_boss.LaserAroundFireState);
        }
        else
        {
            _laserTargetFireChance = _laserTargetFireChanceMin;
            _moveChance += _moveChanceDelta;
            _birthChance += _birthChanceDelta;
            _laserAroundFireChance += _laserAroundFireChanceDelta;
            _boss.SwitchState(_boss.LaserTargetFireState);
        }
    }
}