using UnityEngine;

public class BossRobot : Boss
{
    [Header("Boss Robot")]
    public Animator Animator;
    public Laser Laser;
    public Transform LaserStart;
    public Transform BirthPoint;


    [Header("States")]
    public State<Boss> IdleState;
    public State<Boss> MovementState;
    public State<Boss> LaserAroundFireState;
    public State<Boss> LaserTargetFireState;
    public State<Boss> BirthState;

    public override void Activate()
    {
        Laser.Init(1, 0, 0, LaserStart.right);
        SwitchState(IdleState);
    }   
    
    public void SetLaserRotation(float rotationZ)
    {
        LaserStart.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        Laser.SetDirection(LaserStart.right);
    }

    
    public float GetTargetLaserAngle()
    {
        var targetPosition = (Vector2) Target.position;
        var laserPosition = (Vector2) LaserStart.position;
        
        var direction = targetPosition - laserPosition;

        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return rotationZ;
    }
}
