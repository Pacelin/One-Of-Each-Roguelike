using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Upgrades
{
    [SerializeField] private List<WeaponUpgrade> _weaponUpgrades = new List<WeaponUpgrade>();

    public float GetDamageChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetDamageChanges(weaponValues));
    
    public float GetCritDamageMultiplierChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetCritDamageMultiplierChanges(weaponValues));
    public float GetCritChanceChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetCritChanceChanges(weaponValues));
    public float GetReloadTimeChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetReloadTimeChanges(weaponValues));
    public int GetClipSizeChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetClipSizeChanges(weaponValues));
    public float GetFireRateChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetFireRateChanges(weaponValues));
    public float GetSpreadChanges(WeaponValues weaponValues) =>
        _weaponUpgrades.Sum(upgr => upgr.GetSpreadChanges(weaponValues));
}