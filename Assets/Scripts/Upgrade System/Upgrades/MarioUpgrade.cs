using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Mario")]
public class MarioUpgrade : Upgrade
{
    [SerializeField] private float _scaleMultiply;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        projectile.AddScale(_scaleMultiply);
    }
}