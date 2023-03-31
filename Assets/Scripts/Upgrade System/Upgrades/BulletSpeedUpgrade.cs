using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Bullet Speed Up")]
public class BulletSpeedUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;
    [SerializeField] private float _value;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) 
    {
        projectile.IncreaseSpeed(_value, _percent);
    }
}