using UnityEngine;
using UnityEngine.Events;

public class PositionQuadCollScr : Interactable
{
    [SerializeField] private GameObject ClickActivateGob;
    [SerializeField] private Transform QuadCollTR;
    [SerializeField] private float MaxDistanceFromCenter = 0.75f;
    [SerializeField] private Vector2 MinMaxValue;

    public UnityEvent OnValueChanged;
    public UnityEvent OnClickEvent;
    public UnityEvent OnReleaseEvent;


    // WARNING WARNING 


    // ABSOLUTE SPHAGETTI INCOMING - WARNING - WARNING

    [SerializeField] private bool YouAreMouseSensitivityHuh;
    public static PositionQuadCollScr YouAreMouseSensitivity;
    [SerializeField] private bool YouAreMusicHuh;
    public static PositionQuadCollScr YouAreMusic;
    private void Awake()
    {
        if (YouAreMouseSensitivityHuh)
            YouAreMouseSensitivity = this;

        if (YouAreMusicHuh)
            YouAreMusic = this;
    }


    // FIN. ABSOLUTE CINEMA

    // STAY SAFE OUT THERE


    bool xAxisQuadColl = true;
    public float CurrentValue;

    private void Start()
    {
        if (xAxisQuadColl)
        {
            float t = Mathf.InverseLerp(MinMaxValue.x, MinMaxValue.y, CurrentValue);
            float xPos = Mathf.Lerp(-MaxDistanceFromCenter, MaxDistanceFromCenter, t);

            QuadCollTR.localPosition = new Vector3(xPos, QuadCollTR.localPosition.y, QuadCollTR.localPosition.z);
        }
    }

    float ParalellPositionOfClick;
    bool Clicked = false;
    public override void OnClick()
    {
        if (!GameCursorMechanic.Instance.AskForRaycast(out Ray ray, out RaycastHit hit)) return;

        Clicked = true;

        if (xAxisQuadColl)
        {
            ParalellPositionOfClick = hit.point.x;
        }

        if (!ClickActivateGob.activeSelf)
            ClickActivateGob.SetActive(true);

        OnClickEvent?.Invoke();
    }
    public override void OnClickEnded()
    {
        Clicked = false;

        if (ClickActivateGob.activeSelf)
            ClickActivateGob.SetActive(false);

        OnReleaseEvent?.Invoke();
    }


    private void Update()
    {
        if (!Clicked) return;

        if (!GameCursorMechanic.Instance.AskForRaycast(out Ray ray, out RaycastHit hit)) return;

        float newXPos = hit.point.x;
        float dX = ParalellPositionOfClick - newXPos;
        ParalellPositionOfClick = newXPos;


        if (xAxisQuadColl)
        {
            QuadCollTR.localPosition += dX * new Vector3(1f, 0f, 0f);
            if (QuadCollTR.localPosition.x > MaxDistanceFromCenter)
            {
                QuadCollTR.localPosition = new Vector3(MaxDistanceFromCenter, QuadCollTR.localPosition.y, QuadCollTR.localPosition.z);
            }
            else if (QuadCollTR.localPosition.x < -MaxDistanceFromCenter)
            {
                QuadCollTR.localPosition = new Vector3(-MaxDistanceFromCenter, QuadCollTR.localPosition.y, QuadCollTR.localPosition.z);
            }

            float QuadDistanceFromMinSide = QuadCollTR.localPosition.x + MaxDistanceFromCenter;

            float Value = Mathf.Lerp(MinMaxValue.x, MinMaxValue.y, QuadDistanceFromMinSide / (MaxDistanceFromCenter * 2f));
            CurrentValue = Value;

            OnValueChanged?.Invoke();
        }
    }
}
