using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    public event Action ResumeClicked;
    public event Action RestartClicked;
    public event Action MenuClicked;

    void Awake()
    {
        resumeButton.onClick.AddListener(() => ResumeClicked?.Invoke());
        restartButton.onClick.AddListener(() => RestartClicked?.Invoke());
        menuButton.onClick.AddListener(() => MenuClicked?.Invoke());
        panel.SetActive(false);
    }

    public void Show(bool visible) => panel.SetActive(visible);
}