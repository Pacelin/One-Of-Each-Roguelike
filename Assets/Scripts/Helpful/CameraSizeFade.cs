using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeFade : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private Trigger _trigger;

    private void OnEnable() =>
        _trigger.OnTrigger += OnTrigger;

    private void OnDisable() =>
        _trigger.OnTrigger -= OnTrigger;

    private void OnTrigger() =>
        StartCoroutine(Fade());

    private IEnumerator Fade()
    {
        var maxTime = _sizeCurve.keys[_sizeCurve.length - 1].time;
        for (float time = 0; time < maxTime; time += Time.deltaTime)
        {
            _camera.orthographicSize = _sizeCurve.Evaluate(time);
            yield return null;
        }
    }
}
