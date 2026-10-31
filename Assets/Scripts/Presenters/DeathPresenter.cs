using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPresenter : MonoBehaviour
{
    [SerializeField] DeathView view;

    void Start()
    {
        view.Show(false);
        GameSession.Instance.Level.PlayerDied += OnPlayerDied;
        view.RestartClicked += Restart;
        view.MenuClicked    += BackToMenu;
    }

    void OnDestroy()
    {
        if (GameSession.Instance != null)
            GameSession.Instance.Level.PlayerDied -= OnPlayerDied;
        view.RestartClicked -= Restart;
        view.MenuClicked    -= BackToMenu;
    }

    void OnPlayerDied()
    {
        Time.timeScale = 0f;
        view.Show(true);
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