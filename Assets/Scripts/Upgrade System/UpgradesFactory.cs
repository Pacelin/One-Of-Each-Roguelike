using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesFactory : MonoBehaviour
{
    [SerializeField] private UpgradeHolder _prefab;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private GameObject _upgradesInfo;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _upgradeQualityText;
    [SerializeField] private ItemQualityColors _qualityColors;

    [Header("Chances")]
    [SerializeField] private float[] _commonUpgradesChances = new float[4];
    [SerializeField] private float[] _rareUpgradesChances = new float[4];
    [SerializeField] private float[] _uncommonUpgradesChances = new float[4];
    [SerializeField] private float[] _legendaryUpgradesChances = new float[4];

    private Upgrade[] _commonUpgrades;
    private Upgrade[] _rareUpgrades;
    private Upgrade[] _uncommonUpgrades;
    private Upgrade[] _legendaryUpgrades;

    private void Awake()
    {
        _commonUpgrades = Resources.LoadAll<Upgrade>("Upgrades/Common");
        _rareUpgrades = Resources.LoadAll<Upgrade>("Upgrades/Rare");
        _uncommonUpgrades = Resources.LoadAll<Upgrade>("Upgrades/Uncommon");
        _legendaryUpgrades = Resources.LoadAll<Upgrade>("Upgrades/Legendary");
        _upgradesInfo.SetActive(false);
    }

    public UpgradeHolder SpawnUpgrade(Vector2 position, float[] chances)
    {
        var holder = Instantiate(_prefab, position, Quaternion.identity);
        float p = 0;
        float random = Random.Range(0f, 1f);
        holder.Upgrades = _upgrades;
        holder.UpgradesInfo = _upgradesInfo;
        holder.UpgradeNameText = _upgradeNameText;
        holder.UpgradeDescriptionText = _upgradeDescriptionText;
        holder.UpgradeQualityText = _upgradeQualityText;
        holder.QualityColors = _qualityColors;

        for (int i = 0; i < chances.Length; i++)
        {
            p += chances[i];
            if (random < p)
            {
                holder.SetUpgrade(GetRandomUpgrade(i));
                return holder;
            }
        }

        return holder;
    }

    private Upgrade GetRandomUpgrade(int quality)
    {
        float p = 0;
        float random = Random.Range(0f, 1f);
        float[] chances;

        if (quality == 0)
            chances = _commonUpgradesChances;
        else if (quality == 1)
            chances = _rareUpgradesChances;
        else if (quality == 2)
            chances = _uncommonUpgradesChances;
        else
            chances = _legendaryUpgradesChances;

        for (int i = 0; i < chances.Length; i++)
        {
            p += chances[i];
            if (random <= p)
            {
                if (i == 0)
                    return GetRandomUpgrade(_commonUpgrades);
                if (i == 1)
                    return GetRandomUpgrade(_rareUpgrades);
                if (i == 2)
                    return GetRandomUpgrade(_uncommonUpgrades);
                else
                    return GetRandomUpgrade(_legendaryUpgrades);
            }
        }
        return GetRandomUpgrade(_commonUpgrades);
    }

    private Upgrade GetRandomUpgrade(Upgrade[] array)
    {
        if (array.Length == 0) return null;
        return array[Random.Range(0, array.Length)];
    }
}
