using UnityEngine;

public class SlerpToTargetRot : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float RotationSpeed = 5f;

    private Transform CachedTransform;

    private void Awake()
    {
        CachedTransform = transform;
    }

    private void Update()
    {
        if (Target == null) return;

        CachedTransform.rotation = Quaternion.Slerp(
            CachedTransform.rotation,
            Target.rotation,
            RotationSpeed * Time.deltaTime
        );
    }
}