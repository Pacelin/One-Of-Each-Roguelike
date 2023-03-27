using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Robot/Movement")]
public class BossRobotMovementState : State<Boss>
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _targetOffset;

    private BossRobot _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossRobot) machine;
        _boss.Animator.SetBool("move", true);
        _boss.StartCoroutine(Moving());
    }

    public override void Exit()
    {
        _boss.Animator.SetBool("move", false);
    }

    private IEnumerator Moving()
    {
        var moveTarget = Random.insideUnitCircle * _targetOffset + (Vector2) _boss.Target.position;
        var moveDirection = (moveTarget - _boss.Rigidbody2D.position).normalized;
        var currentDistance = 0f;
        
        while (currentDistance < _moveDistance)
        {
            var delta = Time.fixedDeltaTime * _moveSpeed;
            _boss.Rigidbody2D.MovePosition(_boss.Rigidbody2D.position + moveDirection * delta);
            currentDistance += delta;
            yield return new WaitForFixedUpdate();
        }

        _boss.SwitchState(_boss.IdleState);
    }
}