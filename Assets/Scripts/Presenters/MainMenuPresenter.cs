using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] MainMenuView view;

    void Awake()
    {
        view.ShowMain();
        view.PlayClicked     += OnPlay;
        view.SettingsClicked += OnOpenSettings;
        view.QuitClicked     += OnQuit;
        view.BackClicked     += OnBack;
    }

    void OnDestroy()
    {
        view.PlayClicked     -= OnPlay;
        view.SettingsClicked -= OnOpenSettings;
        view.QuitClicked     -= OnQuit;
        view.BackClicked     -= OnBack;
    }

    void OnPlay()         => SceneManager.LoadScene(1);
    void OnOpenSettings() => view.ShowSettings();
    void OnQuit()         => Application.Quit();
    void OnBack()         => view.ShowMain();
}