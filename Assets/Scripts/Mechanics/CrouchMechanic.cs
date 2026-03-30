using UnityEngine;

public class CrouchMechanic : MonoBehaviour
{
    public static CrouchMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform PlayerBody;

    public float crouchYScale = 0.5f;
    private float originalYScale;

    [HideInInspector] public bool isCrouching = false;
    private void Start()
    {
        if (PlayerBody != null)
            originalYScale = PlayerBody.localScale.y;
    }

    private void Update()
    {
        if (PlayerBody == null) return;

        Vector3 scale = PlayerBody.localScale;

        //if (Input.GetKey(KeyCode.LeftControl))
        if (Input.GetMouseButton(1))
        {
            isCrouching = true;
            scale.y = crouchYScale;

            GroundData.DisableThins();
        }
        else
        {
            isCrouching = false;
            scale.y = originalYScale;

            GroundData.EnableThins();
        }

        PlayerBody.localScale = scale;
    }
}