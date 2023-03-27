using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class RoomLight : MonoBehaviour
{
    [SerializeField] private Light2D[] _lights;
    [SerializeField] private LayerMask _layerMask;
    [Space]
    [SerializeField] private float _fadeInSpeed;
    [SerializeField] private float _fadeOutSpeed;
    [SerializeField] private int _currentObjectsCount;

    private float[] _lightsIntensity;
    private int _objectsCount;

    private void Awake()
    {
        _objectsCount = _currentObjectsCount;
        
        _lightsIntensity = new float[_lights.Length];
        for (int i = 0; i < _lights.Length; i++)
            _lightsIntensity[i] = _lights[i].intensity;
        
        if (_objectsCount == 0)
            for (int i = 0; i < _lights.Length; i++)
                _lights[i].intensity = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MaskContainsLayer(_layerMask, other.gameObject.layer))
            _objectsCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (MaskContainsLayer(_layerMask, other.gameObject.layer))
            _objectsCount--;
    }

    private void Update()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            var targetIntensity = _objectsCount > 0 ? _lightsIntensity[i] : 0;
            _lights[i].intensity = Mathf.MoveTowards(_lights[i].intensity, targetIntensity,
                Time.deltaTime * (targetIntensity > _lights[i].intensity ? _fadeInSpeed : _fadeOutSpeed));
        }
    }

    private bool MaskContainsLayer(LayerMask mask, int layer) =>
        mask == (mask | (1 << layer));
}
