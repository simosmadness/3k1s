using System;
using UnityEngine;

namespace Game.Models
{
    public class LevelModel
    {
        public event Action<float> TimerChanged;
        public event Action<int> EnemiesChanged;
        public event Action LevelWon;
        public event Action PlayerDied;

        public float Time { get; private set; }
        public int EnemiesRemaining { get; private set; }
        public bool IsActive { get; private set; }

        public void Begin(int enemies)
        {
            Time = 0f;
            EnemiesRemaining = enemies;
            IsActive = true;
            TimerChanged?.Invoke(Time);
            EnemiesChanged?.Invoke(EnemiesRemaining);
        }

        public void Tick(float dt)
        {
            if (!IsActive) return;
            Time += dt;
            TimerChanged?.Invoke(Time);
        }

        public void RegisterEnemyKilled()
        {
            if (!IsActive) return;
            EnemiesRemaining = Mathf.Max(0, EnemiesRemaining - 1);
            EnemiesChanged?.Invoke(EnemiesRemaining);
            if (EnemiesRemaining == 0)
            {
                IsActive = false;
                LevelWon?.Invoke();
            }
        }

        public void RegisterPlayerDeath()
        {
            if (!IsActive) return;
            IsActive = false;
            PlayerDied?.Invoke();
        }
    }
}