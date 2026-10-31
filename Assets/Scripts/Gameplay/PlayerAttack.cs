using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float range = 1.2f;
    public float radius = 1.1f;
    public int damage = 1;
    public float cooldown = 0.4f;
    public LayerMask enemyMask;
    public AudioClip swingSound;

    PlayerControls controls;
    float lastAttack = -999f;

    void Awake() => controls = new PlayerControls();
    void OnEnable()  => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!controls.Player.Attack.WasPressedThisFrame()) return;
        if (Time.time < lastAttack + cooldown) return;
        lastAttack = Time.time;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(swingSound, transform.position);

        Vector3 center = transform.position + Vector3.up + transform.forward * range;
        var hits = Physics.OverlapSphere(center, radius, enemyMask);
        foreach (var h in hits)
        {
            var e = h.GetComponentInParent<Enemy>();
            if (e != null) e.TakeDamage(damage);
        }
    }
}