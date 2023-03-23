using UnityEngine;

public class Chest : Pickup
{
    [HideInInspector] public WeaponController Controller;
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
    }

    public override void OnCollectEnter() 
    { 
        _opened = true;
        WeaponHolder.Show();
    }

    public override void OnCollectExit() 
    {
        _opened = false;
        WeaponHolder.Hide();
    }

    private void Update()
    {
        var offset = Time.deltaTime * _openingSpeed;
        var topTargetPositionY = _opened ? _topChestOpenedY : _topChestClosedY;
        
        var pos = TopChest.transform.localPosition;
        pos.y = topTargetPositionY;
        TopChest.transform.localPosition = Vector2.MoveTowards(TopChest.transform.localPosition, pos, offset);
    }
}
