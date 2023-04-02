using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _fadeOutTime;

    private static SceneFade _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start() => FadeIn();

    public static Coroutine SwitchScene(string scene, float delay) =>
        _instance.StartCoroutine(SceneSwitching(scene, delay));

    private static IEnumerator SceneSwitching(string scene, float delay)
    {
        yield return FadeOutAsync();
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        yield return new WaitForSeconds(delay);
        yield return FadeInAsync();
        Time.timeScale = 1;
    }

    public static Coroutine FadeIn() => _instance.StartCoroutine(FadeInAsync());

    public static Coroutine FadeOut() => _instance.StartCoroutine(FadeOutAsync());

    public static IEnumerator FadeInAsync()
    {
        for (float t = 0; t < _instance._fadeInTime; t += Time.unscaledDeltaTime)
        {
            _instance._fadeImage.color = Color.Lerp(Color.black, Color.clear, t / _instance._fadeInTime);
            yield return null;
        }
        _instance._fadeImage.color = Color.clear;
    }

    public static IEnumerator FadeOutAsync()
    {
        for (float t = 0; t < _instance._fadeOutTime; t += Time.unscaledDeltaTime)
        {
            _instance._fadeImage.color = Color.Lerp(Color.clear, Color.black, t / _instance._fadeOutTime);
            yield return null;
        }
        _instance._fadeImage.color = Color.black;
    }
}
