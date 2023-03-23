using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Pistols/Stay")]
public class BossWithPistolsStayState : State<Boss>
{
    [SerializeField] private float _stayTime;
    [SerializeField] private float _angryStayTime;
    [SerializeField] private float _targetFireChance;

    private float _timer;
    private bool _moved;

    private BossWithPistols _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossWithPistols) machine;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        _boss.RotatePistolsToTarget();
    }

    public override void ChangeState()
    {
        if (_boss.IsAngry)
        {
            if (_timer >= _angryStayTime)
            {
                if (_moved)
                {
                    _moved = false;
                    var random = Random.Range(0f, 1f);
                    if (random <= _targetFireChance)
                        _machine.SwitchState(_boss.AngryTargetFireState);
                    else
                        _machine.SwitchState(_boss.AngryAroundFireState);
                }
                else
                {
                    _machine.SwitchState(_boss.AngryMovementState);
                    _moved = true;
                }
            }
        }
        else
        {
            if (_timer >= _stayTime)
            {
                if (_moved)
                {
                    _moved = false;
                    var random = Random.Range(0f, 1f);
                    if (random <= _targetFireChance)
                        _machine.SwitchState(_boss.TargetFireState);
                    else
                        _machine.SwitchState(_boss.AroundFireState);
                }
                else
                {
                    _machine.SwitchState(_boss.MovementState);
                    _moved = true;
                }
            }
        }
    }

    public override void Exit()
    {
        _timer = 0;
    }
}