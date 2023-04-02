using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Wait Serial Fire Rate and Release")]
public class WeaponWaitSerialFireRateAndReleaseState : State<WeaponController>
{
    [SerializeField] private int _serialSize;
    [SerializeField] private float _serialRate;

    private float _timer;
    private int _currentSerialSize;
    private State<WeaponController> _fireState;

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
        if (_currentSerialSize == 0) _currentSerialSize = _serialSize - 1;
        _fireState = from;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
    }
    public override void ChangeState()
    {
        if (_machine.PlayerWantToReload && !_machine.WeaponIsFull)
            _machine.SwitchState(_machine.Weapon.ReloadState);
		
        if (_currentSerialSize > 0)
        {
            if (!_machine.PlayerCanFire)
            {
                _currentSerialSize = 0;
                _machine.SwitchState(_machine.Weapon.IdleState);
            }
            else if (_timer >= 1 / _serialRate)
            {
                _currentSerialSize--;
                _machine.SwitchState(_fireState);
            }
        }
        
        else if (_timer >= 1 / _machine.Weapon.Data.FireRate && 
            !_machine.PlayerIsFire && 
            !_machine.PlayerIsSecondlyFire)
        {
            _machine.SwitchState(_machine.Weapon.IdleState);
        }
    }
}
