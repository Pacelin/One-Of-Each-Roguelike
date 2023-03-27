using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Laser Explosion Fire")]
public class WeaponLaserExplosionFire : WeaponFireState
{
    [SerializeField] private Laser _laserPrefab;
    [SerializeField] private float _startSizeFrom;
    [SerializeField] private float _startSizeTo;
    [SerializeField] private float _endSizeFrom;
    [SerializeField] private float _endSizeTo;
    [SerializeField] private float _explosionTime;
    private Laser _laser;

    private float _timer;

    public override void CreateFirePoints(Transform parent)
    {
        _laser = Instantiate(_laserPrefab, Vector3.zero, Quaternion.identity, parent);
        _laser.DisableLaser();
    }
    public override void RemoveFirePoints()
    {
        Destroy(_laser.gameObject);
    }

    public override void Exit()
    {
        _laser.DisableLaser();
    }

    public override void Fire(Vector2 fireDirection)
    {
        base.Fire(fireDirection);
        fireDirection = SpreadVector(fireDirection);
        _timer = 0;

        _laser.Init(
            _machine.Weapon.Data.Damage, 
            _machine.Weapon.Data.Damage * _machine.Weapon.Data.CritDamageMultiplier,
            _machine.Weapon.Data.CritChance,
            fireDirection);
        
        _machine.Upgrades.ApplyProjectileUpgrades(_laser);
        _laser.SetStartPosition(_machine.MainFirePoint.position);
        _laser.SetDirection(_machine.transform.right);
        _laser.EnableLaser();
        _laser.ApplyDamage();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        _laser.SetSize(
            Mathf.Lerp(_startSizeFrom, _startSizeTo, _timer / _explosionTime),
            Mathf.Lerp(_endSizeFrom, _endSizeTo, _timer / _explosionTime));
    }

    protected override bool FireIsEnded() => _timer >= _explosionTime;
    protected override int GetBulletsCountToFire() => 1;
}