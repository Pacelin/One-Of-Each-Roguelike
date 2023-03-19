using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public static event Action<Weapon> OnFire;
    public static event Action<Weapon> OnReloadStart;
    public static event Action<Weapon, float> OnReloadProgressChanged;
    public static event Action<Weapon> OnReloadEnd;

    public bool IsReloading { get; private set; }
    public bool IsFire { get; private set; }
    public bool ClipIsFull => BulletsRemainingInClip == WeaponValues.GetClipSize(Upgrades);
    public bool ClipIsEmpty => BulletsRemainingInClip == 0;
    public int BulletsRemainingInClip { get; private set; }
    
    [Header("Main Settings")]
    public Sprite Sprite;
    public AudioClip FireSound;
    public AudioClip ReloadSound;
    [Space]
    public WeaponValues WeaponValues;
    [Space]
    public Upgrades Upgrades;
    protected float _rateTimeElapsed;
    protected float _reloadTimeElapsed;

    public abstract void Init(Transform weaponTransform);
    public abstract void Exit(Transform weaponTransform);
    public abstract void CreateProjectiles(Vector2 mousePosition);

    public virtual void Firing(Vector2 mousePosition, bool fire)
    {
        if (IsReloading) 
        {
            var reloadTime = WeaponValues.GetReloadTime(Upgrades);
            _reloadTimeElapsed += Time.deltaTime;
            OnReloadProgressChanged?.Invoke(this, Mathf.Clamp(_reloadTimeElapsed / reloadTime, 0, 1));
            if (_reloadTimeElapsed > reloadTime)
            {
                BulletsRemainingInClip = WeaponValues.GetClipSize(Upgrades);
                OnReloadEnd?.Invoke(this);
                IsReloading = false;
            }
        }

        IsFire = fire;

        if (BulletsRemainingInClip > 0)
        {
            if ((_rateTimeElapsed += Time.deltaTime) < 1 / WeaponValues.GetFireRate(Upgrades)) return;

            if (fire)
            {
                OnFire?.Invoke(this);
                CreateProjectiles(mousePosition);
                BulletsRemainingInClip--;
                _rateTimeElapsed = 0;
            }
        }
    }

    public virtual void StrongFiring(Vector2 mousePosition, bool fire)
    {
    }

    public virtual void Reload()
    {
        if (IsReloading || BulletsRemainingInClip == WeaponValues.GetClipSize(Upgrades)) return;
        OnReloadStart?.Invoke(this);

        IsReloading = true;
        IsFire = false;
        _reloadTimeElapsed = 0;
        _rateTimeElapsed = 0;
    }
}