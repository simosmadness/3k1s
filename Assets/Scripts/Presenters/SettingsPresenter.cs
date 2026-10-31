using Game.Models;
using UnityEngine;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] SettingsView view;

    SettingsModel model;

    void Start()
    {
        model = GameSession.Instance.Settings;

        view.SetSfx(model.SfxVolume);
        view.SetMusic(model.MusicVolume);
        view.SetDifficulty(model.Difficulty);

        view.SfxChanged        += v => model.SetSfx(v);
        view.MusicChanged      += v => model.SetMusic(v);
        view.DifficultyChanged += v => model.SetDifficulty(v);
    }

    void OnDestroy()
    {
        if (view != null) { /* отписки — лямбды, поэтому удобнее унести в OnDestroy через ссылки */ }
    }
}