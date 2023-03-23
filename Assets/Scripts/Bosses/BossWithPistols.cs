using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWithPistols : Boss
{
    [Header("Boss with Pistols")]
    public SpriteRenderer Pistol1;
    public SpriteRenderer Pistol2;
    public Transform PistolFirePoint1;
    public Transform PistolFirePoint2;

    [Space]
    public Sprite FrontSprite;
    public Sprite BackSprite;
    public float AngryHealthThreshold;
    public float AngryHealing;

    [Space]
    public State<Boss> StayState;
    public State<Boss> MovementState;
    public State<Boss> TargetFireState;
    public State<Boss> AroundFireState;
    public State<Boss> AngryMovementState;
    public State<Boss> AngryTargetFireState;
    public State<Boss> AngryAroundFireState;

    public bool IsAngry;

    public override void Activate()
    {
        SwitchState(StayState);
    }

    protected override void OnHealthChanged(float health)
    {
        base.OnHealthChanged(health);
        if (!IsAngry && health < AngryHealthThreshold) 
        {
            IsAngry = true;
            Health.Heal(AngryHealing);
        }
    }

    public void RotatePistolsToTarget()
    {
        var targetPosition = (Vector2) Target.position;

        var pistolPosition1 = new Vector2(Pistol1.transform.position.x, 
            Pistol1.transform.position.y + PistolFirePoint1.localPosition.y);
        var pistolPosition2 = new Vector2(Pistol2.transform.position.x, 
            Pistol2.transform.position.y + PistolFirePoint2.localPosition.y);
        
        var pistolDirection1 = targetPosition - pistolPosition1;
        var pistolDirection2 = targetPosition - pistolPosition2;

        float pistolRotationZ1 = Mathf.Atan2(pistolDirection1.y, pistolDirection1.x) * Mathf.Rad2Deg;
        float pistolRotationZ2 = Mathf.Atan2(pistolDirection2.y, pistolDirection2.x) * Mathf.Rad2Deg;

        SetPistolRotation1(pistolRotationZ1);
        SetPistolRotation2(pistolRotationZ2);
    }
    
    public void SetPistolRotation1(float rotationZ)
    {
        if (Mathf.Abs(rotationZ) > 90f)
            Pistol1.transform.rotation = Quaternion.Euler(180f, 0f, -rotationZ);
        else
            Pistol1.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void SetPistolRotation2(float rotationZ)
    {
        if (Mathf.Abs(rotationZ) > 90f)
            Pistol2.transform.rotation = Quaternion.Euler(180f, 0f, -rotationZ);
        else
            Pistol2.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
