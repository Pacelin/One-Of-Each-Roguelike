using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Wait Serial Fire Rate")]
public class WeaponWaitSerialFireRateState : State<WeaponController>
{
    [SerializeField] private int _serialSize;
    [SerializeField] private float _serialRate;
    [SerializeField] private float _fireRateOffset = 0.5f;

    private float _timer;
    private bool _playerReleaseFire;
    private int _currentSerialSize;
    private State<WeaponController> _fireState;

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
        _playerReleaseFire = false;
        _currentSerialSize--;
        if (_currentSerialSize == -1)
            _currentSerialSize = _serialSize - 1;
        _fireState = from;
    }

    public override void Update()
    {
        if (!_machine.PlayerIsFire && !_machine.PlayerIsSecondlyFire)
            _playerReleaseFire = true;

        _timer += Time.deltaTime;
    }

    public override void ChangeState()
    {
        if (_currentSerialSize > 0)
        {
            if (!_machine.PlayerCanFire)
            {
                _currentSerialSize = 0;
                _machine.SwitchState(_machine.Weapon.IdleState);
            }
            else if (_timer >= 1 / _serialRate)
            {
                _machine.SwitchState(_fireState);
            }
        }
        
        else if (_currentSerialSize <= 0 && _timer >= 1 / (_machine.Weapon.Data.FireRate + (_playerReleaseFire ? _fireRateOffset : 0)))
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
