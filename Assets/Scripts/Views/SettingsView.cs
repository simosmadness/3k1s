using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_Dropdown difficultyDropdown;

    public event Action<float> SfxChanged;
    public event Action<float> MusicChanged;
    public event Action<int> DifficultyChanged;

    void Awake()
    {
        sfxSlider.onValueChanged.AddListener(v => SfxChanged?.Invoke(v));
        musicSlider.onValueChanged.AddListener(v => MusicChanged?.Invoke(v));
        difficultyDropdown.onValueChanged.AddListener(v => DifficultyChanged?.Invoke(v));
    }

    // SetValueWithoutNotify — чтобы не зациклить событие
    public void SetSfx(float v)        => sfxSlider.SetValueWithoutNotify(v);
    public void SetMusic(float v)      => musicSlider.SetValueWithoutNotify(v);
    public void SetDifficulty(int i)   => difficultyDropdown.SetValueWithoutNotify(i);
}