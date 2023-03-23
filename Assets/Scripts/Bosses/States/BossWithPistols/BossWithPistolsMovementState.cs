using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Pistols/Movement")]
public class BossWithPistolsMovementState : State<Boss>
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveDistance;

    private Vector2 _movementDirection;
    private float _currentDistance;

    private BossWithPistols _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossWithPistols) machine;
        _machine.BossSpriteRenderer.sprite = _boss.BackSprite;
        _boss.Pistol1.gameObject.SetActive(false);
        _boss.Pistol2.gameObject.SetActive(false);

        _movementDirection = (_machine.Target.position - _machine.transform.position).normalized;
    }

    public override void FixedUpdate()
    {
        var move = _movementDirection * _movementSpeed * Time.fixedDeltaTime;
        var rotationDelta = _rotationSpeed * Time.fixedDeltaTime;
        if (move.x < 1) rotationDelta = -rotationDelta;

        _machine.Rigidbody2D.MovePosition(_machine.Rigidbody2D.position + move);
        _machine.BossSpriteRenderer.transform.rotation = 
            Quaternion.Euler(0, 0, _machine.BossSpriteRenderer.transform.rotation.eulerAngles.z + rotationDelta);

        _currentDistance += move.magnitude;
    }

    public override void Exit()
    {
        _machine.BossSpriteRenderer.sprite = _boss.FrontSprite;
        _boss.Pistol1.gameObject.SetActive(true);
        _boss.Pistol2.gameObject.SetActive(true);
        _machine.BossSpriteRenderer.transform.rotation = Quaternion.identity;
        _currentDistance = 0;
    }

    public override void ChangeState()
    {
        if (_currentDistance >= _moveDistance)
        {
            _machine.SwitchState(_boss.StayState);
        }
    }
}