using UnityEngine;

public abstract class MovementHandler : MonoBehaviour
{
    public Vector2 MoveDirection { get; protected set; }
    public Vector2 DodgeDirection => Move ? MoveDirection : Vector2.right;
    public bool Dodge { get; protected set; }
    public bool Move => MoveDirection != Vector2.zero;

    public abstract void UpdateHandler();
}