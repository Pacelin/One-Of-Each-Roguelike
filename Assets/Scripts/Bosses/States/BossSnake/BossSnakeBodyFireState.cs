using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Body Fire")]
public class BossSnakeBodyFireState : BossSnakeBodyState
{    
    [SerializeField] private Projectile _bulletPrefab;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        var bullet1 = Instantiate(_bulletPrefab, _boss.BodyFirePoint1.position, Quaternion.identity);
        bullet1.Init(1, 0, 0, _boss.BodyFirePoint1.right);

        var bullet2 = Instantiate(_bulletPrefab, _boss.BofyFirePoint2.position, Quaternion.identity);
        bullet2.Init(1, 0, 0, _boss.BofyFirePoint2.right);
    }

    public override void ChangeState()
    {
        _boss.SwitchState(_boss.BodyPartIdleState);
    }
}
