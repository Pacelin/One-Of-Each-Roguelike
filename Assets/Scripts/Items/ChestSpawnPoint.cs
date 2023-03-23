using UnityEngine;

public class ChestSpawnPoint : MonoBehaviour
{
    [SerializeField] private ChestFactory _factory;

    [Header("Chances")]
    [SerializeField] private float CommonChance;
    [SerializeField] private float RareChance;
    [SerializeField] private float UncommonChance;
    [SerializeField] private float LegendaryChance;

    private void Start()
    {
        _factory.SpawnChest(transform.position, new[] { CommonChance, RareChance, UncommonChance, LegendaryChance });
        Destroy(gameObject);
    }
}
