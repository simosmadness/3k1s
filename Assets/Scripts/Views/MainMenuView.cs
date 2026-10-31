using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Button playButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button backButton;

    public event Action PlayClicked;
    public event Action SettingsClicked;
    public event Action QuitClicked;
    public event Action BackClicked;

    void Awake()
    {
        playButton.onClick.AddListener(() => PlayClicked?.Invoke());
        settingsButton.onClick.AddListener(() => SettingsClicked?.Invoke());
        quitButton.onClick.AddListener(() => QuitClicked?.Invoke());
        backButton.onClick.AddListener(() => BackClicked?.Invoke());
    }

    public void ShowMain()     { mainPanel.SetActive(true);  settingsPanel.SetActive(false); }
    public void ShowSettings() { mainPanel.SetActive(false); settingsPanel.SetActive(true);  }
}