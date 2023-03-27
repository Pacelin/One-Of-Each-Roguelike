using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Pickup
{
    [HideInInspector] public WeaponController Controller;
    [HideInInspector] public GameObject WeaponInfo;
    [HideInInspector] public TMP_Text WeaponQualityText;
    [HideInInspector] public TMP_Text WeaponNameText;
    [HideInInspector] public TMP_Text WeaponDescriptionText;
    [HideInInspector] public Image WeaponImage;
    [HideInInspector] public ItemQualityColors QualityColors;
    public SpriteRenderer TopChest;
    public SpriteRenderer BottomChest;

    public WeaponHolder WeaponHolder;

    [Header("Positions")]
    [SerializeField] private float _topChestClosedY;
    [SerializeField] private float _topChestOpenedY;
    [Space]
    [SerializeField] private float _openingSpeed;

    private bool _opened;

    public override void OnCollect()
    {
        if (WeaponHolder.HoldedWeapon == null) return;
        var oldWeapon = Controller.Weapon;
        Controller.SwitchWeapon(WeaponHolder.HoldedWeapon);
        WeaponHolder.SetWeapon(oldWeapon);
        ShowInfo();
    }

    public override void OnCollectEnter() 
    { 
        _opened = true;
        WeaponHolder.Show();
        ShowInfo();
    }

    public override void OnCollectExit() 
    {
        _opened = false;
        WeaponHolder.Hide();
        WeaponInfo.SetActive(false);
    }

    private void Update()
    {
        var offset = Time.deltaTime * _openingSpeed;
        var topTargetPositionY = _opened ? _topChestOpenedY : _topChestClosedY;
        
        var pos = TopChest.transform.localPosition;
        pos.y = topTargetPositionY;
        TopChest.transform.localPosition = Vector2.MoveTowards(TopChest.transform.localPosition, pos, offset);
    }

    private void ShowInfo()
    {
        if (WeaponHolder.HoldedWeapon != null)
        {
            WeaponNameText.text = WeaponHolder.HoldedWeapon.Name;
            WeaponDescriptionText.text = WeaponHolder.HoldedWeapon.Description;
            WeaponQualityText.text = QualityColors.GetName(WeaponHolder.HoldedWeapon.Quality);
            WeaponQualityText.color = QualityColors.GetColor(WeaponHolder.HoldedWeapon.Quality);
            WeaponImage.sprite = WeaponHolder.HoldedWeapon.Sprite;
            WeaponInfo.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) WeaponInfo.transform);
        }
        else
        {
            WeaponInfo.SetActive(false);
        }
    }
}
