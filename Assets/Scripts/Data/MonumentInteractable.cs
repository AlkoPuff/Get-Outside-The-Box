using UnityEngine;

public class MonumentInteractable : Interactable
{

    [SerializeField] private GameObject MechanicToEnable;
    [SerializeField] private Renderer MonumentRE;
    [SerializeField] private Material ActiveMat;

    [SerializeField] protected AudioClip ClipToPlay;

    [SerializeField] protected float MaxDistance;


    Transform CachedTransform;
    private void Awake()
    {
        CachedTransform = transform;
    }

    public override void OnClick()
    {
        Transform PlayerTR = PlayerLookMechanic.Instance.PlayerBody;

        float Distance = Vector3.Distance(CachedTransform.position, PlayerTR.position);
        if (Distance > MaxDistance) return;


        ActivateMonument();
    }



    bool MonumentActive = false;
    private void ActivateMonument()
    {
        if (MonumentActive) return;
        MonumentActive = true;

        SoundAdder.instance.AddSound(ClipToPlay);

        var mats = MonumentRE.materials;
        mats[1] = ActiveMat;
        MonumentRE.materials = mats;


        MechanicToEnable.SetActive(true);
    }
}
