using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Weapon HoldedWeapon;
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private float _sinusAmplitude;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _showSpeed;

    private float _timer;
    private bool _showed;

    private void OnValidate()
    {
        SetWeapon(HoldedWeapon);
    }
    private void Awake()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0);
        if (HoldedWeapon == null) return;
        SetWeapon(HoldedWeapon);
    }

    private void Update()
    {
        _timer += Time.deltaTime * _animationSpeed;
        
        var pos = -_renderer.localBounds.center;
        pos.y += Mathf.Sin(_timer) * _sinusAmplitude;

        if (HoldedWeapon != null)
            transform.localPosition = pos;

        var targetAlpha = _showed ? 1 : 0;
        var currentAlpha = Mathf.MoveTowards(_renderer.color.a, targetAlpha, Time.deltaTime * _showSpeed);
        _renderer.color = new Color(
            _renderer.color.r,
            _renderer.color.g,
            _renderer.color.b,
            currentAlpha
        );
    }

    public void SetWeapon(Weapon weapon)
    {
        HoldedWeapon = weapon;
        _renderer.sprite = weapon?.Sprite;
    }

    public void Show() => _showed = true;
    public void Hide() => _showed = false;
}
