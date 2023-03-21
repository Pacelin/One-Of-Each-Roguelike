using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : Item
{
    [HideInInspector] public int BulletsInClip;

    [Header("Weapon Settings")]
    public Sprite Sprite;
    public AudioClip FireSound;
    public AudioClip ReloadSound;
    public WeaponData Data;
    public Vector2 MainFirePosition;

    [Space]
    public State<WeaponController> IdleState;
    public WeaponFireState FireState;
    public WeaponFireState SecondFireState;
    public State<WeaponController> WaitFireRateState;
    public State<WeaponController> ReloadState;
}