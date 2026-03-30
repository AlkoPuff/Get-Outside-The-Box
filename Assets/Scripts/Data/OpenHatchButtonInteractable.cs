using UnityEngine;

public class OpenHatchButtonInteractable : Interactable
{

    [SerializeField] private Animator hatchAnime;

    [SerializeField] private float MaxDistance;

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


        HandleOpenHatch();
    }


    bool HatchOpen = false;
    private void HandleOpenHatch()
    {
        if (HatchOpen) return;
        HatchOpen = true;

        hatchAnime.SetTrigger("Open");
    }
}
