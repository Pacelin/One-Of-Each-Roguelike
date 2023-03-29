using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Body Idle")]
public class BossSnakeBodyIdleState : BossSnakeBodyState
{
    [SerializeField] private float _delay;
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

        _boss.SwitchState(_boss.BodyPartFireState);
    }
}