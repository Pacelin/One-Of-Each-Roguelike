using UnityEngine;
using System;

public class MovementInputHandler : MovementHandler
{
    [SerializeField] private KeyCode _upKey = KeyCode.W;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;

    [SerializeField] private KeyCode _dodgeKey = KeyCode.Space;

    public override void UpdateHandler() 
    {
        MoveDirection = Vector2.zero;
        if (Input.GetKey(_upKey))
            MoveDirection += Vector2.up;
        if (Input.GetKey(_leftKey))
            MoveDirection += Vector2.left;
        if (Input.GetKey(_downKey))
            MoveDirection += Vector2.down;
        if (Input.GetKey(_rightKey))
            MoveDirection += Vector2.right;

        MoveDirection.Normalize();
        Dodge = Input.GetKeyDown(_dodgeKey);
    }
}