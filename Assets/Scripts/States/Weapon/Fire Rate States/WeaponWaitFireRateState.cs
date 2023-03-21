using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Wait Fire Rate")]
public class WeaponWaitFireRateState : State<WeaponController>
{
    [SerializeField] private float _fireRateOffset = 0.5f;
    private float _timer;
    private bool _playerReleaseFire;

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
        _playerReleaseFire = false;
    }

    public override void Update()
    {
        if (!_machine.PlayerIsFire && !_machine.PlayerIsSecondlyFire)
            _playerReleaseFire = true;
    }

    public override void ChangeState()
    {
        if ((_timer += Time.deltaTime) >= 
            1 / (_machine.Weapon.Data.FireRate + (_playerReleaseFire ? _fireRateOffset : 0)))
        {   
            if (_machine.PlayerIsFire && _machine.PlayerCanFire)
                _machine.SwitchState(_machine.Weapon.FireState);

            else if (_machine.PlayerIsSecondlyFire && _machine.PlayerCanFire)
                _machine.SwitchState(_machine.Weapon.SecondFireState);
            
            else
                _machine.SwitchState(_machine.Weapon.IdleState);
        }
    }
}
