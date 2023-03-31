using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/All In One")]
public class AllInOneUpgrade : Upgrade
{
    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.Damage = weaponData.Damage * weaponData.ClipSize;
        weaponData.ReloadTime += weaponData.Damage / 300;
        weaponData.ClipSize = 1;
        upgrades.WeaponController.Weapon.BulletsInClip = 1;
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
}