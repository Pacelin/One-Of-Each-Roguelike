using System.Collections;
using UnityEngine;

public class LockOverdamage : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damageDelay;
    [Space]
    [SerializeField] private Color _blinkColor;
    [SerializeField] private float _blinkTime;
    [Space]
    [SerializeField] private SpriteRenderer _blinkedSpriteRenderer;

    private void OnEnable()
    {
        _health.OnDamaged += OnDamage;
    }
    private void OnDisable()
    {
        _health.OnDamaged -= OnDamage;
    }


    private void OnDamage()
    {
        _health.SetCanDamaged(false);
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        var oldColor = _blinkedSpriteRenderer.color;
        var currentTime = 0f;
        while (true)
        {
            for (float t = 0; t < _blinkTime; t += Time.deltaTime, currentTime += Time.deltaTime)
            {
                _blinkedSpriteRenderer.color = Color.Lerp(oldColor, _blinkColor, t / _blinkTime);
                if (currentTime >= _damageDelay)
                {
                    _blinkedSpriteRenderer.color = oldColor;
                    _health.SetCanDamaged(true);
                    yield break;
                }
                yield return null;
            }

            for (float t = 0; t < _blinkTime; t += Time.deltaTime, currentTime += Time.deltaTime)
            {
                _blinkedSpriteRenderer.color = Color.Lerp(_blinkColor, oldColor, t / _blinkTime);
                if (currentTime >= _damageDelay)
                {
                    _blinkedSpriteRenderer.color = oldColor;
                    _health.SetCanDamaged(true);
                    yield break;
                }
                yield return null;
            }
        }
    }
}
