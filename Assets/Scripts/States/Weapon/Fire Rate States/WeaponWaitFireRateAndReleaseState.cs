using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Wait Fire Rate and Release")]
public class WeaponWaitFireRateAndReleaseState : State<WeaponController>
{
    private float _timer;

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
    }

    public override void ChangeState()
    {
        if ((_timer += Time.deltaTime) >= 1 / _machine.Weapon.Data.FireRate && 
            !_machine.PlayerIsFire && 
            !_machine.PlayerIsSecondlyFire)
        {
            _machine.SwitchState(_machine.Weapon.IdleState);
        }
    }
}
