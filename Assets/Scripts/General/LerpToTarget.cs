using UnityEngine;

public class LerpToTarget : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float LerpSpeed = 5f;
    [SerializeField] private bool MaintainOffset = true;

    private Transform CachedTransform;
    private Vector3 Offset;

    private void Awake()
    {
        CachedTransform = transform;

        if (Target != null && MaintainOffset)
        {
            Offset = CachedTransform.position - Target.position;
        }
    }

    private void Update()
    {
        if (Target == null) return;

        Vector3 targetPos = Target.position;
        if (MaintainOffset)
        {
            targetPos += Offset;
        }

        CachedTransform.position = Vector3.Lerp(
            CachedTransform.position,
            targetPos,
            LerpSpeed * Time.deltaTime
        );
    }
}