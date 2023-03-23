using UnityEngine;

public class BossKilledTrigger : Trigger
{
    [SerializeField] private Boss _boss;

    private void OnEnable()
    {
        _boss.OnDeath += Notify;
    }

    private void OnDisable()
    {
        _boss.OnDeath -= Notify;
    }
}