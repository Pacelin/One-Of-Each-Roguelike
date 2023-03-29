using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Head Idle")]
public class BossSnakeHeadIdleState : BossSnakeHeadState
{
    [SerializeField] private float _delay;
    [SerializeField] private float _extraChance;
    private Coroutine _coroutine;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _coroutine = _boss.StartCoroutine(Wait());
    }

    public override void Exit()
    {
        base.Exit();
        _boss.StopCoroutine(_coroutine);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_delay);

        if (Random.Range(0f, 1f) <= _extraChance)
            _boss.SwitchState(_boss.HeadHomingExtraFireState);
        else
            _boss.SwitchState(_boss.HeadHomingFireState);
    }
}