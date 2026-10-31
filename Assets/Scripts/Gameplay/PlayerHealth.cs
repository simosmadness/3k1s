using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public AudioClip hurtSound;

      void Start() // <-- Замени Awake на Start
    {
        if (GameSession.Instance != null)
        {
            GameSession.Instance.PlayerStats.Initialize(maxHealth);
        }
        else
        {
            Debug.LogError("GameSession не найден! Запускай игру из MainMenu!");
        }
    }

    public void TakeDamage(int dmg)
    {
        var stats = GameSession.Instance.PlayerStats;
        if (stats.IsDead) return;

        stats.TakeDamage(dmg);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(hurtSound, transform.position);

        if (stats.IsDead)
            GameSession.Instance.Level.RegisterPlayerDeath();
    }
}