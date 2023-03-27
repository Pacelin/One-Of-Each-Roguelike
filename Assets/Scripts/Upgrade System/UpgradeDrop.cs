using UnityEngine;

public class UpgradeDrop : MonoBehaviour
{
    [SerializeField] private float _dropForce;
    [SerializeField] private float _upgradesCount;
    [SerializeField] private UpgradesFactory _factory;
    [Header("Chances")]
    [SerializeField] private float _commonChances;
    [SerializeField] private float _rareChances;
    [SerializeField] private float _uncommonChances;
    [SerializeField] private float _legendaryChances;

    private void OnDestroy()
    {
        for (int i = 0; i < _upgradesCount; i++)
        {
            var drop = _factory.SpawnUpgrade(transform.position, new [] { _commonChances, _rareChances, _uncommonChances, _legendaryChances });

            drop.Rigidbody.AddForce(Random.insideUnitCircle.normalized * _dropForce);
        }
    }
}