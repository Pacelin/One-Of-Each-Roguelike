using UnityEngine;

public abstract class WeaponFireState : State<WeaponController>
{
    public virtual void CreateFirePoints(Transform parent) { }
    public virtual void RemoveFirePoints() { }

    public virtual void Fire(Vector2 fireDirection)
    {
        _machine.Weapon.BulletsInClip -= GetBulletsCountToFire();
    }
    protected abstract bool FireIsEnded();
    protected abstract int GetBulletsCountToFire();

    public override void Init(WeaponController machine, State<WeaponController> from)
    {
        base.Init(machine, from);
        Fire(machine.transform.right);
    }

    public override void ChangeState()
    {
        if (FireIsEnded())
        {
            if (_machine.PlayerCanFire)
                _machine.SwitchState(_machine.Weapon.WaitFireRateState);
            else
                _machine.SwitchState(_machine.Weapon.IdleState);
        }
    }

    protected Vector2 SpreadVector(Vector2 fireDirection) =>
        fireDirection.Rotate(Random.Range(-_machine.Weapon.Data.Spread / 2, _machine.Weapon.Data.Spread / 2));
}