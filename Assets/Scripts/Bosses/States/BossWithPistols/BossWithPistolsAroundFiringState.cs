using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Pistols/Around Fire")]
public class BossWithPistolsAroundFiringState : State<Boss>
{
    [SerializeField] private Projectile _bulletPrefab;
    [Space]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _startAngle;
    [SerializeField] private float _endAndle;
    [Space]
    [SerializeField] private float _damage;
    [SerializeField] private float _critDamage;
    [SerializeField] private float _critChance;

    private BossWithPistols _boss;

    public override void Init(Boss machine, State<Boss> from)
    {
        base.Init(machine, from);
        _boss = (BossWithPistols) machine;
        Update();
        _boss.StartCoroutine(Firing());
    }

    private void Fire()
    {
        var fireDirection1 = (Vector2) _boss.PistolFirePoint1.right;
        var fireDirection2 = (Vector2) _boss.PistolFirePoint2.right;

        var bullet1 = Instantiate(_bulletPrefab, _boss.PistolFirePoint1.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, fireDirection1)));
        bullet1.Init(_damage, _critDamage, _critChance, fireDirection1);

        var bullet2 = Instantiate(_bulletPrefab, _boss.PistolFirePoint2.position,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, fireDirection2)));
        bullet2.Init(_damage, _critDamage, _critChance, fireDirection2);
    }

    private IEnumerator Firing()
    {
        var time = 1 / _fireRate;
        for (float angle = 0; angle < (_endAndle - _startAngle); angle += Time.deltaTime * _rotationSpeed)
        {
            _boss.SetPistolRotation1(90 + _startAngle + angle);
            _boss.SetPistolRotation2(90 - _startAngle - angle);
            time += Time.deltaTime;
            
            if (time >= 1 / _fireRate)
            {
                time = 0;
                Fire();
            }
            yield return null;
        }

        _machine.SwitchState(_boss.StayState);
    }
}