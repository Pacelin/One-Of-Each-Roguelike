using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _lookAt;

    private void Update()
    {
        var pos = _lookAt.position;
        pos.z = _camera.transform.position.z;
        _camera.transform.position = pos;
    }
}
