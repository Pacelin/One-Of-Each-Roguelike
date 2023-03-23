using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestFactory : MonoBehaviour
{
    [SerializeField] private Chest _prefab;
    [SerializeField] private WeaponController _weaponController;

    [Header("Chests")]
    [SerializeField] private Sprite _commonTop;
    [SerializeField] private Sprite _commonBottom;
    [Space]
    [SerializeField] private Sprite _rareTop;
    [SerializeField] private Sprite _rareBottom;
    [Space]
    [SerializeField] private Sprite _uncommonTop;
    [SerializeField] private Sprite _uncommonBottom;
    [Space]
    [SerializeField] private Sprite _legendaryTop;
    [SerializeField] private Sprite _legendaryBottom;
    [Header("Weapons")]
    [SerializeField] private float[] _commonChestWeaponsChances = new float[4];
    [SerializeField] private float[] _rareChestWeaponsChances = new float[4];
    [SerializeField] private float[] _uncommonChestWeaponsChances = new float[4];
    [SerializeField] private float[] _legendaryChestWeaponsChances = new float[4];

    private Weapon[] _commonWeapons;
    private Weapon[] _rareWeapons;
    private Weapon[] _uncommonWeapons;
    private Weapon[] _legendaryWeapons;

    private List<Weapon> _recievedWeapons = new List<Weapon>();


    private void Awake()
    {
        _commonWeapons = Resources.LoadAll<Weapon>("Weapons/Common");
        _rareWeapons = Resources.LoadAll<Weapon>("Weapons/Rare");
        _uncommonWeapons = Resources.LoadAll<Weapon>("Weapons/Uncommon");
        _legendaryWeapons = Resources.LoadAll<Weapon>("Weapons/Legendary");
    }

    public void SpawnChest(Vector2 position, float[] chances)
    {
        var chest = Instantiate(_prefab, position, Quaternion.identity);
        float p = 0;
        float random = Random.Range(0, 1);
        chest.Controller = _weaponController;

        for (int i = 0; i < chances.Length; i++)
        {
            p += chances[i];
            if (random <= p)
            {
                if (i == 0)
                {
                    chest.TopChest.sprite = _commonTop;
                    chest.BottomChest.sprite = _commonBottom;
                }
                else if (i == 1)
                {
                    chest.TopChest.sprite = _rareTop;
                    chest.BottomChest.sprite = _rareBottom;
                }
                else if (i == 2)
                {
                    chest.TopChest.sprite = _uncommonTop;
                    chest.BottomChest.sprite = _uncommonBottom;
                }
                else
                {
                    chest.TopChest.sprite = _legendaryTop;
                    chest.BottomChest.sprite = _legendaryBottom;
                }
                chest.WeaponHolder.SetWeapon(GetRandomWeapon(i));
                return;
            }
        }
    }

    private Weapon GetRandomWeapon(int quality)
    {
        float p = 0;
        float random = Random.Range(0, 1);
        float[] chances;

        if (quality == 0)
            chances = _commonChestWeaponsChances;
        else if (quality == 1)
            chances = _rareChestWeaponsChances;
        else if (quality == 2)
            chances = _uncommonChestWeaponsChances;
        else
            chances = _legendaryChestWeaponsChances;

        for (int i = 0; i < chances.Length; i++)
        {
            p += chances[i];
            if (random <= p)
            {
                if (i == 0)
                    return GetRandomWeapon(_commonWeapons);
                if (i == 1)
                    return GetRandomWeapon(_rareWeapons);
                if (i == 2)
                    return GetRandomWeapon(_uncommonWeapons);
                else
                    return GetRandomWeapon(_legendaryWeapons);
            }
        }
        return GetRandomWeapon(_commonWeapons);
    }

    private Weapon GetRandomWeapon(Weapon[] array)
    {
        if (array.Length == 0) return null;
        var filtered = array.Where(weapon => !_recievedWeapons.Contains(weapon)).ToArray();
        if (filtered.Length == 0) filtered = array;

        var weapon = filtered[Random.Range(0, filtered.Length)];
        _recievedWeapons.Add(weapon);
        return weapon;
    }
}
