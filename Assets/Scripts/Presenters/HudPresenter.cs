using Game.Models;
using UnityEngine;

public class HudPresenter : MonoBehaviour
{
    [SerializeField] HudView view;

    PlayerStatsModel stats;
    LevelModel level;

    void Start()
    {
        var session = GameSession.Instance;
        stats = session.PlayerStats;
        level = session.Level;

        stats.HealthChanged += OnHealth;
        level.TimerChanged  += OnTimer;
        level.EnemiesChanged += OnEnemies;

        // Начальная синхронизация (модель могла обновиться до подписки)
        if (stats.MaxHealth > 0) OnHealth(stats.CurrentHealth, stats.MaxHealth);
        OnTimer(level.Time);
        OnEnemies(level.EnemiesRemaining);
    }

    void OnDestroy()
    {
        if (stats != null) stats.HealthChanged -= OnHealth;
        if (level != null)
        {
            level.TimerChanged -= OnTimer;
            level.EnemiesChanged -= OnEnemies;
        }
    }

    void OnHealth(int cur, int max) => view.SetHealthNormalized(max > 0 ? (float)cur / max : 0f);
    void OnTimer(float t)           => view.SetTimer(t);
    void OnEnemies(int n)           => view.SetEnemies(n);
}