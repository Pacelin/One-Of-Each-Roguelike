using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Multishot Difficult Fire")]
public class WeaponMultishotDifficultFireState : WeaponFireState
{
    [SerializeField] private Projectile[] _bulletPrefabs;
    [SerializeField] private float[] _fireAngles;
    [SerializeField] private Vector2[] _fireOffsets;

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);

        for (int i = 0; i < _fireAngles.Length; i++)
        {
            var direction = fireDirection.Rotate(_fireAngles[i]);
            direction = SpreadVector(direction);
            
            var bullet = Instantiate(_bulletPrefabs[i], _machine.MainFirePoint.TransformPoint(_fireOffsets[i]),
                Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction)));

            bullet.Init(
                _machine.Weapon.Data.Damage, 
                _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
                _machine.Weapon.Data.CritChance,
                direction);
                
            _machine.Upgrades.ApplyProjectileUpgrades(bullet);
        }

        _machine.NotifyFire(this);
    }

    protected override bool FireIsEnded() => true;
    protected override int GetBulletsCountToFire() => _fireAngles.Length;
}