using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/BasePistol")]
public class BasePistol : Weapon
{
    [Header("Pistol Settings")]
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] protected Vector2 _fireOrigin;

    private Transform _firePoint;

    public override void Init(Transform weaponTransform)
    {
        weaponTransform.transform.ClearChilds();
        
        _firePoint = weaponTransform.AddChild(_fireOrigin);
    }

    public override void Exit(Transform weaponTransform)
    {
        Destroy(_firePoint);
    }

    public override void CreateProjectiles(Vector2 mousePosition)
    {
        var direction = mousePosition - (Vector2)_firePoint.position;
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, direction)));

        bullet.Init(this, direction);
    }
}