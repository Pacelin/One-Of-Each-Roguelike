using UnityEngine;

public abstract class WeaponUpgrade : Item
{
    public virtual float GetDamageChanges(WeaponValues values) => 0;
    public virtual float GetCritDamageMultiplierChanges(WeaponValues values) => 0;
    public virtual float GetCritChanceChanges(WeaponValues values) => 0;
    public virtual float GetReloadTimeChanges(WeaponValues values) => 0;
    public virtual int GetClipSizeChanges(WeaponValues values) => 0;
    public virtual float GetFireRateChanges(WeaponValues values) => 0;
    public virtual float GetSpreadChanges(WeaponValues values) => 0;
}