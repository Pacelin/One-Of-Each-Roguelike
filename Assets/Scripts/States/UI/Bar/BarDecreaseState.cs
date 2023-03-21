using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "2_bar_decrease", menuName = "States/UI/Bar/Decrease")]
public class BarDecreaseState : State<Bar>
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
        while (_machine.Value < _machine.ActualBarValue)
        {
            var barDelta = _barAnimationSpeed * Time.deltaTime * _machine.RangeLength;
            if (_machine.Value > _machine.ActualBarValue - barDelta)
                _machine.ActualBarValue = _machine.Value;
            else
                _machine.ActualBarValue -= barDelta;
            yield return null;
        }
        _machine.SwitchState(_machine.IdleState);
    }

    private IEnumerator AnimateChanges()
    {
        _machine.DecreasingBarValue = _machine.ActualBarValue;
        _machine.DecreasingBar.enabled = true;

        while (_machine.Value < _machine.DecreasingBarValue)
        {
            if (_machine.Value < _machine.ActualBarValue)
            {
                var barDelta = _barAnimationSpeed * Time.deltaTime * _machine.RangeLength;
                if (_machine.Value > _machine.ActualBarValue - barDelta)
                    _machine.ActualBarValue = _machine.Value;
                else
                    _machine.ActualBarValue -= barDelta;
            }
            else
            {
                var barDelta = _changesAnimationSpeed * Time.deltaTime * _machine.RangeLength;
                _machine.DecreasingBarValue -= barDelta;
            }
            yield return null;
        }
        _machine.DecreasingBar.enabled = false;

        _machine.SwitchState(_machine.IdleState);
    }
}