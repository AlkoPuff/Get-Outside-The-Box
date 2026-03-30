using UnityEngine;

public class CheckpointsMechanic : MonoBehaviour
{
    public static CheckpointsMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private AudioClip CheckpointPlaceClip;
    [SerializeField] private float Volume;

    [SerializeField] private Rigidbody PlayerRB;
    private Vector3 CheckpointPosition;
    private Quaternion CheckpointRotation;

    bool CheckpointPlaced = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlaceCheckpoint();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            TeleportToCheckpoint();
        }
    }

    private void PlaceCheckpoint()
    {
        CheckpointPosition = PlayerRB.position;
        CheckpointRotation = PlayerRB.rotation;
        CheckpointPlaced = true;

        SoundAdder.instance.AddSound(CheckpointPlaceClip, Volume, 1f * Vector2.one);
    }
    private void TeleportToCheckpoint()
    {
        if (!CheckpointPlaced) return;

        PlayerRB.position = CheckpointPosition;
        PlayerRB.rotation = CheckpointRotation;
        PlayerRB.linearVelocity = Vector3.zero;
        PlayerRB.angularVelocity = Vector3.zero;
        Physics.SyncTransforms();

        SoundAdder.instance.AddSound(CheckpointPlaceClip, Volume, 0.85f * Vector2.one);
    }
}
