using UnityEngine;

public class LookUpDownMechanic : MonoBehaviour
{
    public static LookUpDownMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Transform CamTarget;


    bool InLook = false;
    private void Update()
    {
        if (EndSceneHandler.Instance != null && EndSceneHandler.Instance.LockLookingUpDown) return;

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.C))
        {
            if (InLook)
            {
                InLook = false;
                CamTarget.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                InLook = true;

                float PosY = GameCursorMechanic.Instance.PositionY;

                if (PosY >= 0)
                {
                    CamTarget.localRotation = Quaternion.Euler(-89.99f, 0f, 0f);

                    if (EndSceneHandler.Instance != null)
                    {
                        EndSceneHandler.Instance.OnLookUp();
                    }
                }
                else
                {
                    CamTarget.localRotation = Quaternion.Euler(89.99f, 0f, 0f);
                }
            }
        }
    }

    public void ResetInLook()
    {
        InLook = false;
    }
}
