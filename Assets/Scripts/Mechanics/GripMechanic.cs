using UnityEngine;

public class GripMechanic : MonoBehaviour
{
    public static GripMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float HardLandSpeed;

    [SerializeField] private AudioClip GripSfx;
    [SerializeField] private float GripVolume;
    [SerializeField] private float GripPitch;
    [SerializeField] private AudioClip HardLandSfx;
    [SerializeField] private float HardLandVolume;
    [SerializeField] private float HardLandPitch;
    [SerializeField] private AudioClip LetGoSfx;
    [SerializeField] private float LetGoVolume;
    [SerializeField] private float LetGoPitch;

    [SerializeField] private float ShakeAmp;
    [SerializeField] private float ShakeTime;

    [HideInInspector] public bool GripMechanicEnabled = false;

    private void OnEnable()
    {
        GripMechanicEnabled = true;
    }
    private void OnDisable()
    {
        GripMechanicEnabled = false;
    }


    public void PlayGripSfx(float Speed)
    {
        if (Speed > HardLandSpeed)
        {
            SoundAdder.instance.AddSound(HardLandSfx, HardLandVolume, new Vector2(HardLandPitch, HardLandPitch));
            CamShakeMechanic.Instance.AddShake(ShakeAmp, ShakeTime);
        }
        else
        {
            SoundAdder.instance.AddSound(GripSfx, GripVolume, new Vector2(GripPitch, GripPitch));
        }
       
    }
    public void PlayLetGoSfx()
    {
        SoundAdder.instance.AddSound(LetGoSfx, LetGoVolume, new Vector2(LetGoPitch, LetGoPitch));
    }
}
