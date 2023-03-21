using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private WeaponController _controller;

    private void OnEnable()
    {
        _controller.OnReload += Reload;
        _image.enabled = false;
    }

    private void OnDisable()
    {
        _controller.OnReload -= Reload;
    }

    private void Reload(float time) =>
        StartCoroutine(Reloading(time));

    private IEnumerator Reloading(float time)
    {
        _image.fillAmount = 0;
        _image.enabled = true;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            _image.fillAmount = t / time;
            yield return null;
        }
        _image.enabled = false;
    }
}