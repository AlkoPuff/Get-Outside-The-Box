using UnityEngine;

public class PlayerJumpMechanic : MonoBehaviour
{
    public static PlayerJumpMechanic Instance;


    [SerializeField] private AudioClip JumpClip;
    [SerializeField] private AudioClip LandClip;
    [SerializeField] private float LandVolume;
    [SerializeField] private float LandPitch;

    [SerializeField] private Rigidbody PlayerRB;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float GroundCheckDistance = 0.1f;
    [SerializeField] private float GroundCheckRadius = 0.3f;
    [SerializeField] private float Gravity;

    [Header("Jump Cooldown")]
    [SerializeField] private float jumpCooldown = 0.05f;
    private float lastJumpTime = -Mathf.Infinity;

    private bool jumpRequested = false;

    private void Awake()
    {
        Instance = this;
        GroundData = GroundData.NoGroundData;

        Physics.gravity = new Vector3(0f, -Gravity, 0f);
    }

    bool PrevGrounded = true;
    private void Update()
    {
        GroundData = IsGrounded();
        if (GroundData.isGrounded && !PrevGrounded)
        {
            if (EndSceneHandler.Instance == null || !EndSceneHandler.Instance.JumpLandSoundDisable)
                SoundAdder.instance.AddSound(LandClip, LandVolume, LandPitch * Vector2.one);
        }
        PrevGrounded = GroundData.isGrounded;

        if (Input.GetKeyDown(KeyCode.Space) && GroundData.isGrounded && Time.time >= lastJumpTime + jumpCooldown)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
            lastJumpTime = Time.time;
        }
    }

    [HideInInspector] public GroundData GroundData = null;
    private GroundData IsGrounded()
    {
        // Center ray
        Ray CenterRay = new Ray(PlayerRB.position, Vector3.down);
        if (Physics.Raycast(CenterRay, out RaycastHit hit, GroundCheckDistance, GroundLayer))
            return GetGroundDataFromHit(hit);

        // 8 rays in a circle around the player
        float angleStep = 45f; // 360 / 8
        for (int i = 0; i < 8; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * GroundCheckRadius;
            Vector3 origin = PlayerRB.position + offset;

            Ray OtherRay = new Ray(origin, Vector3.down);
            if (Physics.Raycast(OtherRay, out RaycastHit hit2, GroundCheckDistance, GroundLayer))
                return GetGroundDataFromHit(hit2);
        }

        return GroundData.NoGroundData;
    }
    private GroundData GetGroundDataFromHit(RaycastHit hit)
    {
        GroundData groundData = hit.collider.gameObject.GetComponent<GroundData>();
        if (groundData == null)
        {
            groundData = GroundData.DefaultGroundData;
        }
        return groundData;
    }

    private void Jump()
    {
        if (CrouchMechanic.Instance != null && CrouchMechanic.Instance.isCrouching) return;

        SoundAdder.instance.AddSound(JumpClip, 0.4f);
        PlayerRB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
}
