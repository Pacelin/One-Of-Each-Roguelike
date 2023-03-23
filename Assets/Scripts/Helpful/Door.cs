using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Trigger _openTrigger;
    [SerializeField] private Trigger _closeTrigger;
    [SerializeField] private Vector2 _openPosition;
    [SerializeField] private Vector2 _closePosition;
    [SerializeField] private float _animationSpeed;

    [Space]
    [SerializeField] private bool _opened;

    private void OnEnable()
    {
        if (_openTrigger != null)
            _openTrigger.OnTrigger += Open;
        if (_closeTrigger != null)
            _closeTrigger.OnTrigger += Close;
    }

    private void OnDisable()
    {
        if (_openTrigger != null)
            _openTrigger.OnTrigger -= Open;
        if (_closeTrigger != null)
            _closeTrigger.OnTrigger -= Close;
    }

    private void Update()
    {
        var targetPosition = _opened ? _openPosition : _closePosition;
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * _animationSpeed);
    }

    private void Open() => _opened = true;
    private void Close() => _opened = false;
}