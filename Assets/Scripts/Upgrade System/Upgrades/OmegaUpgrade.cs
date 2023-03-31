using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Omega")]
public class OmegaUpgrade : Upgrade
{
    [SerializeField] private float _percentDamage;
    [SerializeField] private float _valueDamage;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) 
    {
        if (upgrades.WeaponController.Weapon.BulletsInClip <= 0)
            projectile.SetDamage(projectile.Damage * _percentDamage + _valueDamage);
    }
}