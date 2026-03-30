using UnityEngine;

public class GripPointInteractable : Interactable
{
    [SerializeField] private float MaxDistance;
    [SerializeField] private GameObject SelectedGob;
    [SerializeField] private float Strenght;
    [SerializeField] private float Cooldown;
    private float coolTime = -Mathf.Infinity;

    private int FRAME_FORGIVE = 2; // i would never do such things like this but there is BUUUUG and i dont undesrtansttDD!!
    private int FrameForgiveCount = 0;

    Transform CachedTransform;
    private void Awake()
    {
        CachedTransform = transform;
    }


    bool FullyHover = false;
    private void Update()
    {
        if (GripMechanic.Instance == null || !GripMechanic.Instance.GripMechanicEnabled) return;


        if (!HoveredAt)
        {
            HoverEnd();
            return;
        }

        Transform PlayerTR = PlayerLookMechanic.Instance.PlayerBody;
        float Distance = Vector3.Distance(CachedTransform.position, PlayerTR.position);

        if (Distance > MaxDistance)
        {
            HoverEnd();
        }
        else
        {
            HoverStart();
        }

    }

    private void HoverStart()
    {
        FrameForgiveCount = 0;

        if (!FullyHover)
        {
            FullyHover = true;
            SelectedGob.SetActive(true);
        }
    }
    private void HoverEnd()
    {
        if (FrameForgiveCount < FRAME_FORGIVE)
        {
            FrameForgiveCount++;
            return;
        }
      

        if (FullyHover)
        {
            FullyHover = false;
            SelectedGob.SetActive(false);
        }
    }



    bool HoveredAt = false;
    public override void OnHoverStarted()
    {
        if (GripMechanic.Instance == null || !GripMechanic.Instance.GripMechanicEnabled) return;

        HoveredAt = true;
    }
    public override void OnHoverEnded()
    {
        if (GripMechanic.Instance == null || !GripMechanic.Instance.GripMechanicEnabled) return;

        HoveredAt = false;
    }


    bool ClickHold = false;
    public override void OnClick()
    {
        if (GripMechanic.Instance == null || !GripMechanic.Instance.GripMechanicEnabled) return;
        if (!FullyHover) return;
        if (Time.time < coolTime + Cooldown) return;

        ClickHold = true;

        

        Rigidbody PlayerRB = PlayerMovementMechanic.Instance.PlayerRB;
        float Speed = PlayerRB.linearVelocity.magnitude;
        GripMechanic.Instance.PlayGripSfx(Speed);
        PlayerRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void OnClickEnded()
    {
        if (!ClickHold) return;
        ClickHold = false;

        GripMechanic.Instance.PlayLetGoSfx();

        Rigidbody PlayerRB = PlayerMovementMechanic.Instance.PlayerRB;
        PlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerRB.linearVelocity = new Vector3(0f, Strenght, 0f);

        coolTime = Time.time;
    }
}
