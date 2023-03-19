using System;

[Serializable]
public class WeaponValues
{
    public float BaseDamage;
    public float BaseCritDamageMultiplier;
    public float BaseCritChance;
    public float BaseReloadTime;
    public int BaseClipSize;
    public float BaseFireRate;
    public float BaseSpread;

    public virtual float GetDamage(Upgrades upgrades) =>
        BaseDamage + upgrades.GetDamageChanges(this);
    public virtual float GetCritDamageMultiplier(Upgrades upgrades) =>
        BaseCritDamageMultiplier + upgrades.GetCritDamageMultiplierChanges(this);
    public virtual float GetCritChance(Upgrades upgrades) =>
        BaseCritChance + upgrades.GetCritChanceChanges(this);
    public virtual float GetReloadTime(Upgrades upgrades) =>
        BaseReloadTime + upgrades.GetReloadTimeChanges(this);
    public virtual int GetClipSize(Upgrades upgrades) =>
        BaseClipSize + upgrades.GetClipSizeChanges(this);
    public virtual float GetFireRate(Upgrades upgrades) =>
        BaseFireRate + upgrades.GetFireRateChanges(this);
    public virtual float GetSpread(Upgrades upgrades) =>
        BaseSpread + upgrades.GetSpreadChanges(this);
}