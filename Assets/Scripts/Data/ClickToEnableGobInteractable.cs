using UnityEngine;

public class ClickToEnableGobInteractable : Interactable
{

    [SerializeField] private GameObject GobToEnable;
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

        EnableGob();

    }


    private void EnableGob()
    {
        if (GobToEnable.activeSelf) return;
        GobToEnable.SetActive(true);
    }
}
