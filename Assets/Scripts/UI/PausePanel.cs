using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public bool Closable;
    public bool Opened { get; private set; }
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField] private CanvasGroup _panelGroup;

    private void Awake()
    {
        Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey)) Switch();
    }

    public void GoToMainMenu() =>
        SceneFade.SwitchScene("Main Menu", 0f);

    public void Restart() =>
        SceneFade.SwitchScene("Game", 0f);

    public void Switch()
    {
        if (Opened)
            Close();
        else
            Open();
    }

    public void Open()
    {
        Opened = true;
        _panelGroup.alpha = 1;
        _panelGroup.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void Close()
    {
        if (!Closable) return;
        Opened = false;
        _panelGroup.alpha = 0;
        _panelGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }
}
