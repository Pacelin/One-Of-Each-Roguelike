using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _nextSceneDelay;

    private void OnEnable()
    {
        _fadeImage.gameObject.SetActive(false);
        _playerHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (value <= 0)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        _fadeImage.color = Color.clear;
        _fadeImage.gameObject.SetActive(true);

        for (float t = 0; t < _fadeTime; t += Time.deltaTime)
        {
            _fadeImage.color = Color.Lerp(Color.clear, Color.black, t / _fadeTime);
            yield return null;
        }
        _fadeImage.color = Color.black;

        yield return new WaitForSeconds(_nextSceneDelay);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}