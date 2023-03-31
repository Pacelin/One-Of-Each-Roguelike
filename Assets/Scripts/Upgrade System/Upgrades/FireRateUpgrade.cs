using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Fire Rate Up")]
public class FireRateUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;
    [SerializeField] private float _value;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.FireRate += weaponData.BaseFireRate * _percent + _value;
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
}