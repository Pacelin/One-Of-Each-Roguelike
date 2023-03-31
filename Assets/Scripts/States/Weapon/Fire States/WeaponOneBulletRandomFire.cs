using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/One Bullet Random Fire")]
public class WeaponOneBulletRandomFire : WeaponFireState
{
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private Vector2[] _fireOffsets;
    [SerializeField] private float[] _fireAngles;

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);
        var random = Random.Range(0, _fireOffsets.Length);

        var pos = _machine.MainFirePoint.TransformPoint(_fireOffsets[random]);
        var direction = fireDirection.x < 0 ? fireDirection.Rotate(-_fireAngles[random]) : fireDirection.Rotate(_fireAngles[random]);
        direction = SpreadVector(direction);

        var rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));

        var bullet = Instantiate(_bulletPrefab, pos, rotation);

        bullet.Init(
            _machine.Weapon.Data.Damage, 
            _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
            _machine.Weapon.Data.CritChance,
            direction);

        _machine.Upgrades.ApplyProjectileUpgrades(bullet);
        _machine.NotifyFire(this);
    }

    protected override bool FireIsEnded() => true;
    protected override int GetBulletsCountToFire() => 1;
}