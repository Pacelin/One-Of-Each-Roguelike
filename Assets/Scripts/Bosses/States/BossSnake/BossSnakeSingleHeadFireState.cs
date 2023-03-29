using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Snake/Single Head Fire")]
public class BossSnakeSingleHeadFireState : BossSnakeHeadState
{
    [SerializeField] private HomingMissle _homingMisslePrefab;
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private float _bulletFireRate;
    [SerializeField] private float _homingMissleFireRate;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss.StartCoroutine(HomingFiring());
        _boss.StartCoroutine(BulletFiring());
    }

    private IEnumerator HomingFiring()
    {
        while (true)
        {
            var missle = Instantiate(_homingMisslePrefab, _boss.HeadFirePoint.position, Quaternion.identity);
            missle.Init(1, 0, 0, _boss.HeadFirePoint.right);
            missle.SetTarget(_boss.Target);

            yield return new WaitForSeconds(1 / _homingMissleFireRate);
        }
    }

    private IEnumerator BulletFiring()
    {
        while (true)
       {
            var bullet1 = Instantiate(_bulletPrefab, _boss.HeadFirePoint.position, Quaternion.identity);
            var bullet2 = Instantiate(_bulletPrefab, _boss.HeadFirePoint.position, Quaternion.identity);
            var bullet3 = Instantiate(_bulletPrefab, _boss.HeadFirePoint.position, Quaternion.identity);
            var bullet4 = Instantiate(_bulletPrefab, _boss.HeadFirePoint.position, Quaternion.identity);

            var direction1 = Vector2.Lerp(_boss.HeadFirePoint.right, _boss.HeadFirePoint.up, 0.3f);
            var direction2 = Vector2.Lerp(_boss.HeadFirePoint.right, _boss.HeadFirePoint.up, 0.6f);
            var direction3 = Vector2.Lerp(_boss.HeadFirePoint.right, -_boss.HeadFirePoint.up, 0.3f);
            var direction4 = Vector2.Lerp(_boss.HeadFirePoint.right, -_boss.HeadFirePoint.up, 0.6f);

            bullet1.Init(1, 0, 0, direction1);
            bullet2.Init(1, 0, 0, direction2);
            bullet3.Init(1, 0, 0, direction3);
            bullet4.Init(1, 0, 0, direction4);

            yield return new WaitForSeconds(1 / _bulletFireRate);
        }
    }
}
