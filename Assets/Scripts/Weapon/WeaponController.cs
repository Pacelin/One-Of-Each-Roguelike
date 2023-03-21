using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : StateMachine<WeaponController>
{
    public event Action<WeaponFireState> OnFire;
    public event Action<float> OnReload;

    public Weapon Weapon { get; private set; }

    public SpriteRenderer WeaponSpriteRenderer;
    public Camera Camera;

    [Space]
    public KeyCode ReloadKey;

    [Space]
    public Upgrades Upgrades;
    public Transform MainFirePoint;
    public Transform WeaponHand;

    [Space]
    [SerializeField] private Weapon _initialWeapon;

    public bool PlayerIsFire => Input.GetMouseButton((int) MouseButton.Left) && Weapon.FireState != null;
    public bool PlayerIsSecondlyFire => Input.GetMouseButton((int) MouseButton.Right) && Weapon.SecondFireState != null;
    public bool PlayerWantToReload => Input.GetKeyDown(ReloadKey) || (Input.GetMouseButtonDown((int) MouseButton.Left) && !PlayerCanFire);
    public bool PlayerCanFire => Weapon.BulletsInClip > 0;
    public bool WeaponIsFull => Weapon.BulletsInClip == Weapon.Data.ClipSize;

    private void OnValidate()
    {
        if (_initialWeapon != null)
            WeaponSpriteRenderer.sprite = _initialWeapon.Sprite;
    }

    protected override void Start()
    {
        SetWeapon(_initialWeapon);

        _currentState = Weapon.IdleState;
        _currentState.Init(this, null);
    }

    protected override void Update()
    {
        if (Weapon != null)
            transform.rotation = GetWeaponRotation();
        base.Update();
    }

    public void SwitchWeapon(Weapon newWeapon)
    {
        ResetWeapon(Weapon);
        SetWeapon(newWeapon);
        SwitchState(Weapon.IdleState);
    }

    public void NotifyFire(WeaponFireState fire) =>
        OnFire?.Invoke(fire);
    public void NotifyReload(float time) =>
        OnReload?.Invoke(time);

    private void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
        Upgrades.ApplyWeaponUpgrades();

        Weapon.BulletsInClip = Weapon.Data.ClipSize;

        Weapon.FireState?.CreateFirePoints(transform);
        Weapon.SecondFireState?.CreateFirePoints(transform);

        MainFirePoint.localPosition = Weapon.MainFirePosition;
    }

    private void ResetWeapon(Weapon weapon)
    {
        Weapon.FireState?.RemoveFirePoints();
        Weapon.SecondFireState?.RemoveFirePoints();
    }

    public Quaternion GetWeaponRotation()
    {
        var mousePos = (Vector2) Camera.ScreenToWorldPoint(Input.mousePosition);
        var myPos = new Vector2(transform.position.x, transform.position.y + MainFirePoint.localPosition.y);
        var direction = mousePos - myPos;

        float rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(rotateZ) > 90f)
            return Quaternion.Euler(180f, 0f, -rotateZ);
        else
            return Quaternion.Euler(0f, 0f, rotateZ);
    }
}
