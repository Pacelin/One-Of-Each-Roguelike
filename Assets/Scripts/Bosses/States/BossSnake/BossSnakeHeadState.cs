using UnityEngine;

public abstract class BossSnakeHeadState : State<Boss>
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _snakeRotationSpeed;
    [SerializeField] private float _snakeRotationAmplitude;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _raycastMask;

    protected BossSnakePart _boss;

    private float _targetRotationZ;
    private float _currentRotationZ;
    private float _currentSnakeRotationZ;
    private int _currentSnakeMovementDirection = 1;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossSnakePart) machine;
        _boss.BossSpriteRenderer.sprite = _boss.HeadSprite;

        if (from is BossSnakeHeadState state)
        {
            _targetRotationZ = state._targetRotationZ;
            _currentRotationZ = state._currentRotationZ;
            _currentSnakeRotationZ = state._currentSnakeRotationZ;
            _currentSnakeMovementDirection = state._currentSnakeMovementDirection;
        }
        else
        {
            _targetRotationZ = Random.Range(-180f, 180f);
        }
    }

    public override void FixedUpdate()
    {
        Move();
        CheckCorrectMovementDirection();
    }

    private void Move()
    {
        _currentRotationZ = Mathf.MoveTowardsAngle(
            _currentRotationZ, 
            _targetRotationZ, 
            _rotationSpeed * Time.fixedDeltaTime);

        _currentSnakeRotationZ = Mathf.MoveTowardsAngle(
            _currentSnakeRotationZ,
            _currentSnakeMovementDirection * _snakeRotationAmplitude,
            _snakeRotationSpeed * Time.fixedDeltaTime);
        
        if (Mathf.Abs(_currentSnakeRotationZ) == _snakeRotationAmplitude)
            _currentSnakeMovementDirection *= -1;

        _boss.SetRotation(_currentRotationZ + _currentSnakeRotationZ);

        _boss.Rigidbody2D.MovePosition(_boss.Rigidbody2D.position + 
            (Vector2) _boss.transform.right * _movementSpeed * Time.fixedDeltaTime);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_boss.transform.position, _boss.transform.position + (Vector3) GetDirection(_targetRotationZ) * _raycastDistance);
    }

    private void CheckCorrectMovementDirection()
    {
        if (Physics2D.Raycast(_boss.transform.position, GetDirection(_targetRotationZ), _raycastDistance, _raycastMask))
        {
            if (Physics2D.Raycast(_boss.transform.position, GetDirection(_targetRotationZ + 90), _raycastDistance, _raycastMask))
                _targetRotationZ += Random.Range(-180f, 0f);
            else
                _targetRotationZ += Random.Range(0f, 180f);
        }
    }

    private Vector2 GetDirection(float rotation)
    {
        var sin = Mathf.Sin(rotation * Mathf.Deg2Rad);
        var cos = Mathf.Cos(rotation * Mathf.Deg2Rad);

        return new Vector2(cos, sin);
    }

}