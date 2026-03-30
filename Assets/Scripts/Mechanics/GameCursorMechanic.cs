using System;
using UnityEngine;

public class GameCursorMechanic : MonoBehaviour
{
    public static GameCursorMechanic Instance;
    private void Awake()
    {
        Instance = this;

        MainCameraTR = MainCamera.transform;
    }

    [SerializeField] private RectTransform GameCursorRT;
    [SerializeField] private float Sensitivity;
    [SerializeField] private LayerMask CursorLayerMask;

    [SerializeField] private Camera MainCamera; // assign your main camera in inspector
    [SerializeField] private Transform MainCameraTR;

    [HideInInspector] public float PositionY = 0f;

    private void Update()
    {
        HandleCursorPosition();
        CastRayFromCursor();

        if (Input.GetMouseButtonUp(0))
        {
            HandleClickRelease();
        }
    }

    RectTransform parentRT;
    float halfHeight;
    private void Start()
    {
        parentRT = GameCursorRT.parent as RectTransform;
        halfHeight = parentRT.rect.height / 2f;
    }

    private void HandleCursorPosition()
    {
        //float MoveY = WebGLMouseRaw.GetMousePositionDelta().y * Sensitivity;
        float MoveY = Input.mousePositionDelta.y * Sensitivity * GlobalMouseManager.GlobalSensitivityMultiplier;
#if UNITY_WEBGL && !UNITY_EDITOR
        MoveY *= WebGLMouseRaw.WebSensitivityMultiplier;
#endif
        PositionY += MoveY;

        if (parentRT != null)
        {
            PositionY = Mathf.Clamp(PositionY, -halfHeight, halfHeight);
            GameCursorRT.anchoredPosition = new Vector2(0, PositionY);
        }
    }

    public bool AskForRaycast(out Ray ray, out RaycastHit hit) // true if hit
    {
        // UI anchoredPosition TO screen point
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GameCursorRT.parent as RectTransform,
            GameCursorRT.position,
            MainCamera,
            out screenPoint
        );

        // wrld point TO near plane
        Vector3 cursorWorldPos = MainCamera.ScreenToWorldPoint(new Vector3(
            GameCursorRT.position.x,
            GameCursorRT.position.y,
            MainCamera.nearClipPlane
        ));

        // cam TO cursor & ray
        ray = MainCamera.ScreenPointToRay(GameCursorRT.position);
        if (Physics.Raycast(ray, out hit, 100f, CursorLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void CastRayFromCursor()
    {
        if (MainCamera == null || GameCursorRT == null) return;

        if (AskForRaycast(out Ray ray, out RaycastHit hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            HitCase(hit);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);

            HoverEndForAllPrevious();
        }
    }

    public Vector3 GetLookNormal()
    {
        float depth = 1f;
        Vector3 worldPos = MainCamera.ScreenToWorldPoint(
            new Vector3(GameCursorRT.position.x, GameCursorRT.position.y, depth)
        );

        return (worldPos - MainCameraTR.position).normalized;
    }

    private void HoverEndForAllPrevious()
    {
        foreach (var prev in lastFrameInteractables)
            prev.OnHoverEnded();

        lastFrameInteractables = new Interactable[0];
    }

    private Interactable[] lastFrameInteractables = new Interactable[0];
    private Interactable[] currentClickedInteractables = new Interactable[0];

    private void HitCase(RaycastHit hit)
    {
        Interactable[] interactables = hit.collider.gameObject.GetComponents<Interactable>();
        if (interactables.Length == 0)
        {
            HoverEndForAllPrevious();
            return;
        }


        foreach (var prev in lastFrameInteractables)
        {
            bool stillHovering = false;
            foreach (var curr in interactables)
            {
                if (curr == prev)
                {
                    stillHovering = true;
                    break;
                }
            }
            if (!stillHovering)
                prev.OnHoverEnded();
        }

        foreach (var curr in interactables)
        {
            bool wasHovering = false;
            foreach (var prev in lastFrameInteractables)
            {
                if (prev == curr)
                {
                    wasHovering = true;
                    break;
                }
            }
            if (!wasHovering)
                curr.OnHoverStarted();
        }

        lastFrameInteractables = interactables;

        foreach (Interactable interactable in interactables)
        {
            interactable.OnHover();
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentClickedInteractables = interactables;

            foreach (Interactable interactable in interactables)
            {
                interactable.OnClick();
            }
        }
    }

    private void HandleClickRelease()
    {
        if (currentClickedInteractables.Length == 0) return;

        foreach (var interactable in currentClickedInteractables)
        {
            if (interactable != null)
                interactable.OnClickEnded();
        }

        currentClickedInteractables = new Interactable[0];
    }
}