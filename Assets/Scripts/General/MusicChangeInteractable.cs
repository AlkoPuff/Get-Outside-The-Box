using UnityEngine;

public class MusicChangeInteractable : Interactable
{
    [SerializeField] private int MusicIndex;
    [SerializeField] private bool DisableOnClick;
    [SerializeField] protected float MaxDistance;

    Transform CachedTransform;
    private void Awake()
    {
        CachedTransform = transform;
    }

    public override void OnClick()
    {
        if (!enabled) return;
        Transform PlayerTR = PlayerLookMechanic.Instance.PlayerBody;

        float Distance = Vector3.Distance(CachedTransform.position, PlayerTR.position);
        if (Distance > MaxDistance) return;

        PlayeMUsico();

    }


    private void PlayeMUsico()
    {
        MusicManager.Instance.PlayMusic(MusicIndex);

        if (DisableOnClick)
            enabled = false;
    }
}
