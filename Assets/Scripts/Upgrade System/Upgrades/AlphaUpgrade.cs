using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Alpha Bullets")]
public class AlphaUpgrade : Upgrade
{
    [SerializeField] private float _percentDamage;
    [SerializeField] private float _valueDamage;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) 
    {
        if (upgrades.WeaponController.Weapon.BulletsInClip >= weaponData.ClipSize - 1)
            projectile.SetDamage(projectile.Damage * _percentDamage + _valueDamage);
    }
}