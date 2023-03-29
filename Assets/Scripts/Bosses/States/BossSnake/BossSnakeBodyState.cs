using UnityEngine;

public abstract class BossSnakeBodyState : State<Boss>
{
    [SerializeField] private float _distanceToPart;
    protected BossSnakePart _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossSnakePart) machine;
    }

    public override void FixedUpdate()
    {
        _boss.SetRotationByDirection(_boss.NextPart.transform);

        var myPos = (Vector2) _boss.transform.position;
        var nextPartPos = (Vector2) _boss.NextPart.transform.position;
        var directionFromNextpart = (myPos - nextPartPos).normalized;
        var delta = directionFromNextpart * _distanceToPart;

        _boss.Rigidbody2D.MovePosition(nextPartPos + delta);
    }
}