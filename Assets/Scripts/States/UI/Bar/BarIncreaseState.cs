using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "1_bar_increase", menuName = "States/UI/Bar/Increase")]
public class BarIncreaseState : State<Bar>
{
    [SerializeField] private BarAnimationType _animationType;
    [SerializeField] private float _barAnimationSpeed;
    [SerializeField] private float _changesAnimationSpeed;

     
    public override void Init(Bar machine, State<Bar> from)
    {
        base.Init(machine, from);
        if (_animationType == BarAnimationType.None)
        {
            machine.ActualBarValue = machine.Value;
            machine.SwitchState(machine.IdleState);
            return;
        }
        
        if (_animationType == BarAnimationType.AnimateBar)
            machine.StartCoroutine(AnimateBar());
        else
            machine.StartCoroutine(AnimateChanges());
    }

    private IEnumerator AnimateBar() 
    {
        while (_machine.Value > _machine.ActualBarValue)
        {
            var barDelta = _barAnimationSpeed * Time.deltaTime * _machine.RangeLength;
            if (_machine.Value < _machine.ActualBarValue + barDelta)
                _machine.ActualBarValue = _machine.Value;
            else
                _machine.ActualBarValue += barDelta;
            yield return null;
        }
        _machine.SwitchState(_machine.IdleState);
    }

    private IEnumerator AnimateChanges()
    {
        _machine.IncreasingBarValue = _machine.ActualBarValue;
        _machine.IncreasingBar.enabled = true;

        while (_machine.Value > _machine.ActualBarValue)
        {
            if (_machine.Value > _machine.IncreasingBarValue)
            {
                var barDelta = _barAnimationSpeed * Time.deltaTime * _machine.RangeLength;
                if (_machine.Value < _machine.IncreasingBarValue + barDelta)
                    _machine.IncreasingBarValue = _machine.Value;
                else
                    _machine.IncreasingBarValue += barDelta;
            }
            else
            {
                var barDelta = _changesAnimationSpeed * Time.deltaTime * _machine.RangeLength;
                if (_machine.Value < _machine.ActualBarValue + barDelta)
                    _machine.ActualBarValue = _machine.Value;
                else
                    _machine.ActualBarValue += barDelta;
            }
            yield return null;
        }
        _machine.IncreasingBar.enabled = false;

        _machine.SwitchState(_machine.IdleState);
    }
}