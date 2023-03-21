using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Chargeable Fire")]
public class WeaponChargableFireState : WeaponFireState
{
    [SerializeField] private WeaponChargeData[] _charges;

    private bool _ended;
    private float _timer;
    private int _currentCharge;

    public override void CreateFirePoints(Transform parent)
    {
        foreach(var charge in _charges)
            if (charge.FireState != null)
                charge.FireState.CreateFirePoints(parent);
    }
    public override void RemoveFirePoints()
    {
        foreach(var charge in _charges)
            if (charge.FireState != null)
                charge.FireState.RemoveFirePoints();
    }

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        _timer = 0;
        _currentCharge = 0;
        _ended = false;
    }

    public override void Exit()
    {
        _machine.WeaponSpriteRenderer.sprite = _machine.Weapon.Sprite;
        _machine.transform.localPosition = Vector2.zero;
    }

    public override void Update()
    {
        if (_machine.PlayerIsFire) 
        {
            _timer += Time.deltaTime;
            if (_currentCharge < _charges.Length && _timer >= _charges[_currentCharge].Time)
                NextCharge();
            
            if (_currentCharge > 0)
                _machine.transform.localPosition = _machine.transform.rotation * _charges[_currentCharge - 1].WeaponPosition;
        }
        else
        {
            _ended = true;
            if (_currentCharge == 0 ||
                _charges[_currentCharge - 1].FireState == null) return;

            _machine.SwitchState(_charges[_currentCharge - 1].FireState);
        }
    }

    protected override bool FireIsEnded() => _ended;
    protected override int GetBulletsCountToFire() => 0;

    private void NextCharge()
    {
        if (_charges[_currentCharge].WeaponSprite != null)
            _machine.WeaponSpriteRenderer.sprite = _charges[_currentCharge].WeaponSprite;

        _currentCharge = _currentCharge + 1;
    }
}