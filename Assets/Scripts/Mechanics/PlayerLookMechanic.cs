using UnityEngine;

public class PlayerLookMechanic : MonoBehaviour
{
    public static PlayerLookMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform PlayerBody;
    [SerializeField] private float MouseSensitivity = 200f;

    private void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * GlobalMouseManager.GlobalSensitivityMultiplier;
#if UNITY_WEBGL && !UNITY_EDITOR
    mouseX *= WebGLMouseRaw.WebSensitivityMultiplier_ForAxis;
#endif
        mouseX *= MouseSensitivity;

        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}