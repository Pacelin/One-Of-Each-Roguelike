using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Increment")]
public class IncrementUpgrade : Upgrade
{
    [Range(0, 1)] [SerializeField] private float _percent;
    [SerializeField] private float _value;
    private int _stacks;

    public override void ApplyWeaponUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) 
    { 
        weaponData.Damage += weaponData.BaseDamage * (_stacks * _percent) + (_stacks * _value);
    }

    public override void ApplyPlayerUpgrade(Upgrades upgrades, WeaponData weaponData, PlayerData playerData) { }
    public override void ApplyProjectileUpgrade(Projectile projectile, Upgrades upgrades, WeaponData weaponData, PlayerData playerData)
    {
        weaponData.Damage += weaponData.BaseDamage * _percent + _value;
        _stacks++;
    }
}