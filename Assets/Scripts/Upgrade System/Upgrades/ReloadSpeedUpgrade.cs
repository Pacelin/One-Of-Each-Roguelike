using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Reload Speed Up")]
public class ReloadSpeedUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.ReloadTime -= weaponData.ReloadTime * _percent;
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
}