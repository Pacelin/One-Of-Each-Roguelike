using UnityEngine;

public class ChangingBullet : Bullet
{
    [SerializeField] private Sprite _spriteWhenCrit;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _crit;
    public override void Init(float damage, float critDamage, float critChance, Vector2 direction)
    {
        base.Init(damage, critDamage, critChance, direction);
        _crit = Random.Range(0f, 1f) < _critChance;
        if (_crit)
            _spriteRenderer.sprite = _spriteWhenCrit;
    }

    protected override void Hit(Health health)
    {
        if (_crit)
            health.TakeDamage(_critDamage);
        else 
            health.TakeDamage(_damage);

        if (IsPoisoned) health.SetPoison(5, 10);
    }    
}