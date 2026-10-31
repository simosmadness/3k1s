using System;

namespace Game.Models
{
    public class PlayerStatsModel
    {
        public event Action<int, int> HealthChanged; // (current, max)

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public void Initialize(int max)
        {
            MaxHealth = max;
            CurrentHealth = max;
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (CurrentHealth <= 0) return;
            CurrentHealth = Math.Max(0, CurrentHealth - amount);
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }
    }
}