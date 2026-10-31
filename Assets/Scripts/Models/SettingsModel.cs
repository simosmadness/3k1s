using System;
using UnityEngine;

namespace Game.Models
{
    public class SettingsModel
    {
        public event Action<float> SfxChanged;
        public event Action<float> MusicChanged;
        public event Action<int> DifficultyChanged;

        public float SfxVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public int Difficulty { get; private set; } // 0=Easy 1=Normal 2=Hard

        public float EnemyDamageMultiplier =>
            Difficulty == 0 ? 0.5f : Difficulty == 2 ? 2f : 1f;

        public SettingsModel()
        {
            SfxVolume = PlayerPrefs.GetFloat("sfx", 1f);
            MusicVolume = PlayerPrefs.GetFloat("music", 0.6f);
            Difficulty = PlayerPrefs.GetInt("difficulty", 1);
        }

        public void SetSfx(float v)        { SfxVolume = v; PlayerPrefs.SetFloat("sfx", v);        SfxChanged?.Invoke(v); }
        public void SetMusic(float v)      { MusicVolume = v; PlayerPrefs.SetFloat("music", v);    MusicChanged?.Invoke(v); }
        public void SetDifficulty(int i)   { Difficulty = i; PlayerPrefs.SetInt("difficulty", i);  DifficultyChanged?.Invoke(i); }
    }
}