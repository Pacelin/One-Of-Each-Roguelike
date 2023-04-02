using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Exit() =>
        Application.Quit();

    public void Play() =>
        SceneFade.SwitchScene("Game", 0f);
}
