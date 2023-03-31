using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Extra")]
public class ExtraUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _chance;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        if (projectile is not Laser)
            if (Random.Range(0f, 1f) < _chance)
            {
                var copy = Instantiate(projectile);
                copy.Init(projectile.Damage, projectile.CritDamage, projectile.CritChance, projectile.Direction);
            }
    }
}