using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/One Bullet Fire")]
public class WeaponOneBulletFire : WeaponFireState
{
    [SerializeField] private Projectile _bulletPrefab;

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);
        fireDirection = SpreadVector(fireDirection);

        var bullet = Instantiate(_bulletPrefab, _machine.MainFirePoint.position,
            Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, fireDirection)));
        _machine.Upgrades.ApplyProjectileUpgrades(bullet);

        bullet.Init(_machine.Weapon, fireDirection);
        _machine.NotifyFire(this);
    }

    protected override bool FireIsEnded() => true;
    protected override int GetBulletsCountToFire() => 1;
}