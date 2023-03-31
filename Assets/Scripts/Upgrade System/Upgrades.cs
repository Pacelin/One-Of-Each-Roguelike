using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public WeaponController WeaponController;
    public Player Player;

    [SerializeField] private List<Upgrade> _list;

    public void ApplyWeaponUpgrades()
    {
        WeaponController.Weapon.Data.Reset();
        var sorted = _list.OrderByDescending(up => up.UpgradePriority);
        foreach(var upgrade in sorted)
            upgrade.ApplyWeaponUpgrade(this, WeaponController.Weapon.Data, Player.Data);
    }
    
    public void ApplyPlayerUpgrades()
    {
        Player.Data.Reset();
        var sorted = _list.OrderByDescending(up => up.UpgradePriority);
        foreach(var upgrade in sorted)
            upgrade.ApplyPlayerUpgrade(this, WeaponController.Weapon.Data, Player.Data);
        Player.Data.Update();
    }

    public void ApplyProjectileUpgrades(Projectile projectile)
    {
        var sorted = _list.OrderByDescending(up => up.UpgradePriority);
        foreach(var upgrade in sorted)
            upgrade.ApplyProjectileUpgrade(projectile, this, WeaponController.Weapon.Data, Player.Data);
    }

    public int GetUpgradesCount(Type type) =>
        _list.Count(upgrade => upgrade.GetType() == type);

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade.Uniq && _list.Contains(upgrade)) return;

        _list.Add(upgrade);
        ApplyPlayerUpgrades();
        ApplyWeaponUpgrades();
    }
    public bool CanUpgrade(Upgrade upgrade)
    {
        return !upgrade.Uniq || !_list.Contains(upgrade);
    }
}