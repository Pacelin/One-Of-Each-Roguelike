using UnityEngine;

[CreateAssetMenu(menuName = "Items/Quality Colors")]
public class ItemQualityColors : ScriptableObject
{
    [SerializeField] private Color _common;
    [SerializeField] private Color _rare;
    [SerializeField] private Color _uncommon;
    [SerializeField] private Color _legendary;

    public Color GetColor(ItemQuality quality)
    {
        if (quality == ItemQuality.Common)
            return _common;
        else if (quality == ItemQuality.Rare)
            return _rare;
        else if (quality == ItemQuality.Uncommon)
            return _uncommon;
        else
            return _legendary;
    }

    public string GetName(ItemQuality quality)
    {
        if (quality == ItemQuality.Common)
            return "Обычный";
        else if (quality == ItemQuality.Rare)
            return "Редкий";
        else if (quality == ItemQuality.Uncommon)
            return "Необычный";
        else
            return "Легендарный";
    }
}