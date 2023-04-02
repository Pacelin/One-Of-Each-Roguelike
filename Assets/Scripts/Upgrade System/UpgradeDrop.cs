using UnityEngine;

public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private float _dropForce;
    [SerializeField] private float _upgradesCount;
    [SerializeField] private UpgradesFactory _factory;
    [Header("Chances")]
    [SerializeField] private float _commonChances;
    [SerializeField] private float _rareChances;
    [SerializeField] private float _uncommonChances;
    [SerializeField] private float _legendaryChances;

    private void OnEnable()
    {
        _boss.OnDeath += OnBossDeath;
    }

    private void OnDisable()
    {
        _boss.OnDeath -= OnBossDeath;
    }

    private void OnBossDeath()
    {
        for (int i = 0; i < _upgradesCount; i++)
        {
            var drop = _factory.SpawnUpgrade(transform.position, new [] { _commonChances, _rareChances, _uncommonChances, _legendaryChances });

            drop.Rigidbody.AddForce(Random.insideUnitCircle.normalized * _dropForce);
        }
    }
}