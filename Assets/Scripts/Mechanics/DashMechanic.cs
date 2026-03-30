using UnityEngine;

public class DashMechanic : MonoBehaviour
{
    public static DashMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private AudioClip Sfx;
    [SerializeField] private float Volume;
    [SerializeField] private float Pitch;

    [SerializeField] private Rigidbody PlayerRB;


    [SerializeField] private float dashFOVIncrease = 10f;


    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.15f;

    [Header("Dash Cooldown")]
    [SerializeField] private float dashCooldown = 0.05f;
    private float lastDashTime = -Mathf.Infinity;

    private bool dashRequested = false;
    [HideInInspector] public bool isDashing = false;
    private float dashTimer = 0f;

    private Vector3 dashDirection;

    private bool CanDashFromGrounded = false;

    private void Update()
    {
        if (PlayerJumpMechanic.Instance.GroundData.isGrounded)
        {
            CanDashFromGrounded = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            Time.time >= lastDashTime + dashCooldown &&
            CanDashFromGrounded &&
           !CrouchMechanic.Instance.isCrouching &&
           !(CrouchDashMechanic.Instance && CrouchDashMechanic.Instance.isDashing))
        {
            CanDashFromGrounded = false;
            dashRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (dashRequested)
        {
            StartDash();
            dashRequested = false;
            lastDashTime = Time.time;
        }

        HandleDash();
    }

    private void StartDash()
    {

        isDashing = true;
        dashTimer = dashDuration;

        SoundAdder.instance.AddSound(Sfx, Volume, new Vector2(Pitch, Pitch));

        PlayerFOVManager.Instance.AddToFOV(dashFOVIncrease, dashDuration);

        dashDirection = PlayerRB.transform.forward.normalized;
    }

    private void HandleDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;

            PlayerRB.linearVelocity = dashDirection * dashSpeed;

            if (dashTimer <= 0f)
                isDashing = false;
        }
    }
}
