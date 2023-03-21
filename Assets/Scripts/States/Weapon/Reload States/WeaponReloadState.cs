using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Reload")]
public class WeaponReloadState : State<WeaponController>
{
    private float _timer;

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
        _machine.NotifyReload(_machine.Weapon.Data.ReloadTime);
    }

    public override void ChangeState()
    {
        if ((_timer += Time.deltaTime) >= _machine.Weapon.Data.ReloadTime)
        {
            _machine.Weapon.BulletsInClip = _machine.Weapon.Data.ClipSize;
             _machine.SwitchState(_machine.Weapon.IdleState);
        }
    }
}
