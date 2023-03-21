using System;
using UnityEngine;

[Serializable]
public class WeaponChargeData
{
    public float Time;
    public Vector2 WeaponPosition;
    public Sprite WeaponSprite;
    public WeaponFireState FireState;
}