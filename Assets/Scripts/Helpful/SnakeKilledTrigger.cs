using UnityEngine;

public class SnakeKilledTrigger : Trigger
{
    [SerializeField] private Boss[] _bossParts;
    private int _currentDeaths = 0;

    private void OnEnable()
    {
        foreach (var boss in _bossParts)
            boss.OnDeath += OnPartDeath;
    }

    private void OnDisable()
    {
        foreach (var boss in _bossParts)
            boss.OnDeath -= OnPartDeath;
    }

    private void OnPartDeath()
    {
        _currentDeaths++;
        if (_currentDeaths == _bossParts.Length) Notify();
    }
}