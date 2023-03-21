using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Idle")]
public class WeaponIdleState : State<WeaponController>
{
    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        machine.WeaponSpriteRenderer.sprite = machine.Weapon.Sprite;
    }

    public override void ChangeState()
    {
        if (_machine.PlayerWantToReload && !_machine.WeaponIsFull)
            _machine.SwitchState(_machine.Weapon.ReloadState);

        else if (_machine.PlayerIsFire && _machine.PlayerCanFire)
            _machine.SwitchState(_machine.Weapon.FireState);

        else if (_machine.PlayerIsSecondlyFire && _machine.PlayerCanFire)
            _machine.SwitchState(_machine.Weapon.SecondFireState);
    }
}
