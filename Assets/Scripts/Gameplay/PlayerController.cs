using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float rotationSpeed = 15f;
    public float gravity = -20f;

    [Header("Jump")]
    public float jumpHeight = 1.4f;

    [Header("Dash")]
    public float dashSpeed = 18f;
    public float dashDuration = 0.18f;
    public float dashCooldown = 1f;

    CharacterController cc;
    Transform cam;
    PlayerControls controls;

    Vector3 velocity;
    bool grounded;
    bool isDashing;
    Vector3 dashDir;
    float dashTimer;
    float lastGroundDash = -999f;
    bool airDashUsed;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        controls = new PlayerControls();
        cam = Camera.main != null ? Camera.main.transform : transform;
    }

    void OnEnable()  => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        if (Time.timeScale == 0f) return;

        grounded = cc.isGrounded;
        if (grounded)
        {
            if (velocity.y < 0f) velocity.y = -2f;
            airDashUsed = false; // сброс воздушного рывка при касании земли
        }

        // Ввод направления относительно камеры
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        Vector3 camFwd = cam.forward; camFwd.y = 0; camFwd.Normalize();
        Vector3 camRight = cam.right; camRight.y = 0; camRight.Normalize();
        Vector3 moveDir = camFwd * input.y + camRight * input.x;
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        if (!isDashing && moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        // Прыжок
        if (grounded && !isDashing && controls.Player.Jump.WasPressedThisFrame())
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Рывок
        if (controls.Player.Dash.WasPressedThisFrame())
        {
            Vector3 dir = moveDir.sqrMagnitude > 0.01f ? moveDir.normalized : transform.forward;
            if (grounded && Time.time >= lastGroundDash + dashCooldown)
            {
                StartDash(dir);
                lastGroundDash = Time.time;
            }
            else if (!grounded && !airDashUsed)
            {
                StartDash(dir);
                airDashUsed = true;
            }
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f) isDashing = false;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 horizontal = isDashing ? dashDir * dashSpeed : moveDir * walkSpeed;
        cc.Move((horizontal + Vector3.up * velocity.y) * Time.deltaTime);
    }

    void StartDash(Vector3 dir)
    {
        isDashing = true;
        dashDir = dir;
        dashTimer = dashDuration;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}