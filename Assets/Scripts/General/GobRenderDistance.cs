using UnityEngine;

public class GobRenderDistance : MonoBehaviour
{

    [SerializeField] private GameObject GobToTurn;
    private Transform GobToTurnTR;
    [SerializeField] private Transform DistanceFrom;
    [SerializeField] private float CheckUpdate;

    [SerializeField] private float MaxDistance;
    float updateT = 0f;


    private void Start()
    {
        GobToTurnTR = GobToTurn.transform;
    }
    private void Update()
    {
        updateT += Time.deltaTime;
        if (updateT < CheckUpdate) return;
        updateT = 0f;

        float Distance = Vector3.Distance(DistanceFrom.position, GobToTurnTR.position);
        if (Distance > MaxDistance)
        {
            if (GobToTurn.activeSelf)
                GobToTurn.SetActive(false);
        }
        else
        {
            if (!GobToTurn.activeSelf)
                GobToTurn.SetActive(true);
        }
    }
}
