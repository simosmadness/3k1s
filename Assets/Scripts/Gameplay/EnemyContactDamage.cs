using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage = 10;
    public float range = 1.6f;
    public float interval = 1f;
    float nextHit;
    Transform player;
    PlayerHealth playerHealth;

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) { player = p.transform; playerHealth = p.GetComponent<PlayerHealth>(); }
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;
        if (Time.time < nextHit) return;
        if (Vector3.Distance(transform.position, player.position) > range) return;

        nextHit = Time.time + interval;
        float mult = GameSession.Instance.Settings.EnemyDamageMultiplier;
        playerHealth.TakeDamage(Mathf.RoundToInt(damage * mult));
    }
}