using UnityEngine;

public class Laser : Projectile
{
    [SerializeField] protected LineRenderer _lineRenderer;
    [SerializeField] private int _penetrationsCount;
    [SerializeField] private float _maxDistance = 5;

    private Vector2 _startPos;
    private bool _applyDamage;

    public void EnableLaser() =>
        _lineRenderer.enabled = true;
    public void DisableLaser() =>
        _lineRenderer.enabled = false;
    public void SetStartPosition(Vector2 startPos) =>
        _startPos = startPos;

    public void SetSize(float startSize, float endSize)
    {
        _lineRenderer.startWidth = startSize;
        _lineRenderer.endWidth = endSize;
    }

    public void ApplyDamage() =>
        _applyDamage = true;

    private void Update()
    {
        Vector2 endPos = _startPos + _fireDirection * _maxDistance;
        var raycast = Physics2D.RaycastAll(_startPos, _fireDirection, _maxDistance);
        
        var currentPenetrations = 0;
        foreach (var hit in raycast)
        {
            if (hit.transform.CompareTag("wall"))
            {
                endPos = hit.point;
                break;
            }
            else
            {
                var health = hit.collider.gameObject.GetComponent<Health>();
                if (health == null || health.Type != _damageableHealthType)
                    continue;
                

                if (_applyDamage)
                {
                    Hit(health);
                }
                
                if (_penetrationsCount == currentPenetrations)
                {
                    endPos = hit.point;
                    break;
                }

                currentPenetrations++;
            }
        }

        _lineRenderer.SetPositions(new Vector3[] { _startPos, endPos });
        _applyDamage = false;
    }
}
