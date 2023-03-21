using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public abstract void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData);
    public abstract void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData);
    public abstract void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData);
}