using UnityEngine;

public class PlayerMovementMechanic : MonoBehaviour
{
    public static PlayerMovementMechanic Instance;
    private void Awake()
    {
        WalkSource.Play();
        WalkSource.Pause();
        Instance = this;
    }

    [SerializeField] private AudioSource WalkSource;
    [SerializeField] private Animator PlaAnime;

    public Rigidbody PlayerRB;
    [SerializeField] private float MoveForce = 10f;
    [SerializeField] private float HorizontalDrag = 5f;

    private Vector2 MoveInput = Vector2.zero;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float MoveForwards = 0f;
        float MoveRight = 0f;

        if (Input.GetKey(KeyCode.W))
            MoveForwards++;
        if (Input.GetKey(KeyCode.S))
            MoveForwards--;
        if (Input.GetKey(KeyCode.D))
            MoveRight++;
        if (Input.GetKey(KeyCode.A))
            MoveRight--;

        MoveInput = new Vector2(MoveRight, MoveForwards).normalized;
    }

    private void FixedUpdate()
    {
        if (PlayerJumpMechanic.Instance.GroundData == null) return;

        HandleMovement_F();
        ApplyHorizontalDrag_F();
    }

    private void HandleMovement_F()
    {
        if (MoveInput.magnitude == 0f || CrouchMechanic.Instance.isCrouching || !PlayerJumpMechanic.Instance.GroundData.isGrounded)
        {
            if (WalkSource.isPlaying)
                WalkSource.Pause();

            PlaAnime.SetBool("Running", false);
        }
        else
        {
            if (!WalkSource.isPlaying)
                WalkSource.UnPause();

            PlaAnime.SetBool("Running", true);
        }


        if (CrouchMechanic.Instance.isCrouching) return;

        if (MoveInput.magnitude == 0f)
            if (PlayerJumpMechanic.Instance.GroundData.ignoreFriction)
                return;

        Vector3 forward = PlayerRB.transform.forward;
        Vector3 right = PlayerRB.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * MoveInput.y + right * MoveInput.x;

        PlayerRB.AddForce(moveDirection * MoveForce, ForceMode.Force);
    }

    private void ApplyHorizontalDrag_F()
    {
        if (!PlayerJumpMechanic.Instance.GroundData.isGrounded && CrouchMechanic.Instance.isCrouching) return;
        if (PlayerJumpMechanic.Instance.GroundData.ignoreFriction && MoveInput.magnitude == 0f) return;

        Vector3 velocity = PlayerRB.linearVelocity;
        velocity.x /= 1f + HorizontalDrag * Time.fixedDeltaTime;
        velocity.z /= 1f + HorizontalDrag * Time.fixedDeltaTime;
        PlayerRB.linearVelocity = new Vector3(velocity.x, velocity.y, velocity.z);
    }
}