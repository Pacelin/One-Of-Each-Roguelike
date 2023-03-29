using System.Collections;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    [SerializeField] private Trigger _enterTrigger;
    [SerializeField] private Trigger _exitTrigger;
    [SerializeField] private Health _bossHealth;
    [SerializeField] private Bar _bossHealthBar;
    [SerializeField] private CanvasGroup _healthBarGroup;

    private void Awake()
    {
        _bossHealth.SetCanDamaged(false);
    }

    private void OnEnable()
    {
        _enterTrigger.OnTrigger += Show;
        _exitTrigger.OnTrigger += Hide;
    }

    private void OnDisable()
    {
        _enterTrigger.OnTrigger -= Show;
        _exitTrigger.OnTrigger -= Hide;
    }

    public void Show()
    {
        _bossHealthBar.Notificator = _bossHealth;
        _bossHealth.SetCanDamaged(true);
        StartCoroutine(ChangeAlpha(0, 1, 1));
    }

    public void Hide()
    {
        StartCoroutine(ChangeAlpha(1, 0, 1));
    }

    private IEnumerator ChangeAlpha(float from, float to, float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            _healthBarGroup.alpha = Mathf.Lerp(from, to, t / time);
            yield return null;
        }
        _healthBarGroup.alpha = to;
    }
}
