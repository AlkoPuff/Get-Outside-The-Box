using System.Collections;
using UnityEngine;

public class EndSceneHandler : MonoBehaviour
{
    public static EndSceneHandler Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject CheckpointMechanicGob;

    [SerializeField] private Transform CamTarget;
    [SerializeField] private Transform CamTR;

    [SerializeField] private Rigidbody PlayerRB;
    [SerializeField] private Transform TeleportTargetTR;

    [SerializeField] private GameObject BoxMap;
    [SerializeField] private GameObject EndMap;

    [HideInInspector] public bool LockLookingUpDown = false;
    [HideInInspector] public bool JumpLandSoundDisable = false;

    bool LookedUp = false;
    public void OnLookUp()
    {
        CheckpointMechanicGob.SetActive(false);

        if (LookedUp) return;
        LookedUp = true;

        StartCoroutine(HandleFunction());
    }


    IEnumerator HandleFunction()
    {
        LockLookingUpDown = true;
        JumpLandSoundDisable = true;

        yield return new WaitForSeconds(1f);

        BoxMap.SetActive(false);
        EndMap.SetActive(true);

        PlayerRB.position = TeleportTargetTR.position; // tele player
        PlayerRB.transform.forward = TeleportTargetTR.forward;
        Physics.SyncTransforms();

        CamTarget.localRotation = Quaternion.Euler(0f, 0f, 0f); // reset camera rot
        Vector3 CamForward = CamTR.forward;
        CamForward.y = 0f;
        CamTR.forward = CamForward;
        LookUpDownMechanic.Instance.ResetInLook();
    }
}
