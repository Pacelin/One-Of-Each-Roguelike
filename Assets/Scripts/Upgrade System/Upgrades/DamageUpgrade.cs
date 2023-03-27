using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Damage Up")]
public class DamageUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;
    [SerializeField] private float _value;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.Damage += weaponData.BaseDamage * _percent + _value;
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
}