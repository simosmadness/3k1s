using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] PauseView view;

    bool isPaused;

    void Awake()
    {
        view.ResumeClicked  += Resume;
        view.RestartClicked += Restart;
        view.MenuClicked    += BackToMenu;
    }

    void OnDestroy()
    {
        view.ResumeClicked  -= Resume;
        view.RestartClicked -= Restart;
        view.MenuClicked    -= BackToMenu;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            Toggle();
    }

    void Toggle()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        view.Show(isPaused);
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        view.Show(false);
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}