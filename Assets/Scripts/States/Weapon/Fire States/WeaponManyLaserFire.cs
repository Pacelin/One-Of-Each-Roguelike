using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Many Laser Fire")]
public class WeaponManyLaserFire : WeaponFireState
{
    [SerializeField] private Laser[] _laserPrefabs;
    [SerializeField] private Vector2[] _laserPositions;

    private Laser[] _lasers;
    private float _timer;

    public override void CreateFirePoints(Transform parent)
    {
        _lasers = new Laser[_laserPrefabs.Length];
        for (int i = 0; i < _laserPrefabs.Length; i++)
        {
            _lasers[i] = Instantiate(_laserPrefabs[i], _laserPositions[i], Quaternion.identity, parent);
            _lasers[i].DisableLaser();
        }
    }
    public override void RemoveFirePoints()
    {
        for (int i = 0; i < _lasers.Length; i++)
            Destroy(_lasers[i].gameObject);
    }

    public override void Exit()
    {
        for (int i = 0; i < _lasers.Length; i++)
            _lasers[i].DisableLaser();
    }

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);

        for (int i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].Init(
                _machine.Weapon.Data.Damage, 
                _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
                _machine.Weapon.Data.CritChance,
                fireDirection);
            _machine.Upgrades.ApplyProjectileUpgrades(_lasers[i]);
            _lasers[i].EnableLaser();
        }
        
        Update();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;

        for (int i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].SetStartPosition(_machine.MainFirePoint.TransformPoint(_laserPositions[i]));
            _lasers[i].SetDirection(_machine.transform.right);
        }
        
        if (_timer >= 1 / _machine.Weapon.Data.FireRate)
        {
            _machine.Weapon.BulletsInClip--;
            _timer = 0;
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].Init(
                    _machine.Weapon.Data.Damage, 
                    _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
                    _machine.Weapon.Data.CritChance,
                    _machine.transform.right);
                _machine.Upgrades.ApplyProjectileUpgrades(_lasers[i]);

                _lasers[i].ApplyDamage();
            }
            _machine.NotifyFire(this);
        }
    }

    protected override bool FireIsEnded() => !_machine.PlayerCanFire || !_machine.PlayerIsFire;
    protected override int GetBulletsCountToFire() => 0;
}