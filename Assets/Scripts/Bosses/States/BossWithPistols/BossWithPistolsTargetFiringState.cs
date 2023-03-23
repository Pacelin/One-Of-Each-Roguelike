using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Pistols/Target Fire")]
public class BossWithPistolsTargetFiringState : State<Boss>
{
    [SerializeField] private Projectile _bulletPrefab;
    [Space]
    [SerializeField] private int _firesCount;
    [SerializeField] private int _seriesCount;
    [SerializeField] private float _firesRate;
    [SerializeField] private float _seriesRate;
    [Space]
    [SerializeField] private float _damage;
    [SerializeField] private float _critDamage;
    [SerializeField] private float _critChance;

    private BossWithPistols _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossWithPistols) machine;
        Update();
        _boss.StartCoroutine(Firing());
    }

    public override void Update()
    {
        _boss.RotatePistolsToTarget();
    }

    private void Fire()
    {
        var fireDirection1 = (Vector2) _boss.PistolFirePoint1.right;
        var fireDirection2 = (Vector2) _boss.PistolFirePoint2.right;

        var bullet1 = Instantiate(_bulletPrefab, _boss.PistolFirePoint1.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, fireDirection1)));
        bullet1.Init(_damage, _critDamage, _critChance, fireDirection1);

        var bullet2 = Instantiate(_bulletPrefab, _boss.PistolFirePoint2.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, fireDirection2)));
        bullet2.Init(_damage, _critDamage, _critChance, fireDirection2);
    }

    private IEnumerator Firing()
    {
        for (int serie = 0; serie < _seriesCount; serie++)
        {
            for (int fire = 0; fire < _firesCount - 1; fire++)
            {
                Fire();
                yield return new WaitForSeconds(1 / _firesRate);
            }
            Fire();
            yield return new WaitForSeconds(1 / _seriesRate);
        }

        _machine.SwitchState(_boss.StayState);
    }
}