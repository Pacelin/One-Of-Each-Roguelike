using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private GameObject _returnButton;
    [SerializeField] private PausePanel _pausePanel;

    private void OnEnable()
    {
        _playerHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (value <= 0)
        {
            _returnButton.SetActive(false);
            _pausePanel.Closable = false;
            _pausePanel.Open();
        }
    }
}