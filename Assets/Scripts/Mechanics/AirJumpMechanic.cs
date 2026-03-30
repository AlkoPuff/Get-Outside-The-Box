using UnityEngine;

public class AirJumpMechanic : MonoBehaviour
{
    public static AirJumpMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private AudioClip Sfx;
    [SerializeField] private float Volume;

    [SerializeField] private float airJumpFOVincrease;
    [SerializeField] private float airJumpFOVincreaseTIME;
    [SerializeField] private Rigidbody PlayerRB;
    [SerializeField] private float Strenght;
    [SerializeField] private float airjumpCooldown;

    private bool CanAirJump;
    private bool airjumpRequested = false;
    private float lastJumpTime = -Mathf.Infinity;

    private void Update()
    {
        if (PlayerJumpMechanic.Instance.GroundData == null) return;

        if (PlayerJumpMechanic.Instance.GroundData.isGrounded)
        {
            CanAirJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) &&
           Time.time >= lastJumpTime + airjumpCooldown &&
           CanAirJump &&
           !CrouchMechanic.Instance.isCrouching)
        {
            CanAirJump = false;
            airjumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (airjumpRequested)
        {
            StartAirJump();
            airjumpRequested = false;
            lastJumpTime = Time.time;
        }
    }

    private void StartAirJump()
    {
        SoundAdder.instance.AddSound(Sfx, Volume);

        Vector3 Velocity = PlayerRB.linearVelocity;
        Velocity.y = Strenght;

        PlayerFOVManager.Instance.AddToFOV(airJumpFOVincrease, airJumpFOVincreaseTIME);

        PlayerRB.linearVelocity = Velocity;
    }
}
