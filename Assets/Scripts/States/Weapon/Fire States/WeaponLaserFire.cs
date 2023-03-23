using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Laser Fire")]
public class WeaponLaserFire : WeaponFireState
{
    [SerializeField] private Laser _laserPrefab;
    private Laser _laser;

    private float _timer;

    public override void CreateFirePoints(Transform parent)
    {
        _laser = Instantiate(_laserPrefab, Vector3.zero, Quaternion.identity, parent);
    }
    public override void RemoveFirePoints()
    {
        Destroy(_laser);
    }

    public override void Exit()
    {
        _laser.DisableLaser();
    }

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);
        fireDirection = SpreadVector(fireDirection);
        _machine.Upgrades.ApplyProjectileUpgrades(_laser);

        _laser.Init(
            _machine.Weapon.Data.Damage, 
            _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
            _machine.Weapon.Data.CritChance,
            fireDirection);
        Update();
        _laser.EnableLaser();
    }

    public override void Update()
    {
        _laser.SetStartPosition(_machine.MainFirePoint.position);
        _laser.SetDirection(_machine.transform.right);

        _timer += Time.deltaTime;
        if (_timer >= 1 / _machine.Weapon.Data.FireRate)
        {
            _timer = 0;
            _laser.ApplyDamage();
            _machine.NotifyFire(this);
            _machine.Weapon.BulletsInClip--;
        }
    }

    protected override bool FireIsEnded() => !_machine.PlayerCanFire || !_machine.PlayerIsFire;
    protected override int GetBulletsCountToFire() => 0;
}