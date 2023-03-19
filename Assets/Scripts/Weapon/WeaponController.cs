using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponController : MonoBehaviour
{
    public Vector2 MousePosition => _camera.ScreenToWorldPoint(Input.mousePosition);

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;

    private void OnValidate()
    {
        _renderer.sprite = _weapon.Sprite;
    }

    private void Start()
    {
        _weapon.Init(transform);
    }

    private void Update()
    {
        _weapon.Firing(MousePosition, Input.GetMouseButton((int)MouseButton.Left));
        _weapon.StrongFiring(MousePosition, Input.GetMouseButton((int)MouseButton.Right));

        if ((Input.GetMouseButtonDown((int)MouseButton.Left) && _weapon.ClipIsEmpty) ||
            Input.GetKeyDown(_reloadKey))
            _weapon.Reload();
    }

    public void SetWeapon(Weapon weapon)
    {
        _renderer.sprite = weapon.Sprite;

        _weapon = weapon;
        _weapon.Init(transform);
    }
}
