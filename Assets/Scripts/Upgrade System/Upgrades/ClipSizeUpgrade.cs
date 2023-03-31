using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Clip Size Up")]
public class ClipSizeUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;
    [SerializeField] private int _value;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.ClipSize += (int) (weaponData.BaseClipSize * _percent) + _value;
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
}