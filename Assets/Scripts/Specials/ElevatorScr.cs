using UnityEngine;

public class ElevatorScr : MonoBehaviour
{
    [SerializeField] private Transform[] ElevatorPositions;
    [SerializeField] private Transform ElevatorMover;
    [SerializeField] private int TargetIndex;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    private void Update()
    {
        MoveElevator();
    }

    public void MoveUp()
    {
        TargetIndex++;
        ClampTargetIndex();
    }

    public void MoveDown()
    {
        TargetIndex--;
        ClampTargetIndex();
    }

    private void ClampTargetIndex()
    {
        if (TargetIndex < 0)
            TargetIndex = 0;
        else if (TargetIndex >= ElevatorPositions.Length)
            TargetIndex = ElevatorPositions.Length - 1;
    }

    private void MoveElevator()
    {
        Transform target = ElevatorPositions[TargetIndex];

        Vector3 current = ElevatorMover.position;
        Vector3 destination = target.position;

        Vector3 direction = (destination - current).normalized;
        float step = moveSpeed * Time.deltaTime;

        if (Vector3.Distance(current, destination) <= step)
        {
            ElevatorMover.position = destination;
        }
        else
        {
            ElevatorMover.position = current + direction * step;
        }
    }
}
