using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public AudioClip hitSound;
    public AudioClip deathSound;
    int health;

    void Awake() => health = maxHealth;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(hitSound, transform.position);
        if (health <= 0) Die();
    }

    void Die()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(deathSound, transform.position);
        GameSession.Instance.Level.RegisterEnemyKilled();
        Destroy(gameObject);
    }
}