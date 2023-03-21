using System;

[Serializable]
public class WeaponData
{
    public float Damage { get; set; }
    public float CritDamageMultiplier { get; set; }
    public float CritChance { get; set; }
    public float ReloadTime { get; set; }
    public int ClipSize { get; set; }
    public float FireRate { get; set; }
    public float Spread { get; set; }

    public float BaseDamage;
    public float BaseCritDamageMultiplier;
    public float BaseCritChance;
    public float BaseReloadTime;
    public int BaseClipSize;
    public float BaseFireRate;
    public float BaseSpread;

    public void Reset()
    {
        Damage = BaseDamage;
        CritDamageMultiplier = BaseCritDamageMultiplier;
        CritChance = BaseCritChance;
        ReloadTime = BaseReloadTime;
        ClipSize = BaseClipSize;
        FireRate = BaseFireRate;
        Spread = BaseSpread;
    }
}
