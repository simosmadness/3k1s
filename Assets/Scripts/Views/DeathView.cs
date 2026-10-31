using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathView : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    public event Action RestartClicked;
    public event Action MenuClicked;

    void Awake()
    {
        restartButton.onClick.AddListener(() => RestartClicked?.Invoke());
        menuButton.onClick.AddListener(() => MenuClicked?.Invoke());
        panel.SetActive(false);
    }

    public void Show(bool visible) => panel.SetActive(visible);
}