using UnityEngine;

public class FinalEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject MovementStuff;
    [SerializeField] private GameObject RunningSFXGob;
    [SerializeField] private GameObject colloGob;
    [SerializeField] private Transform camTarget;
    [SerializeField] private Transform newCamTargetParent;
    [SerializeField] private Animator FinalAnimator;

    private void OnTriggerEnter(Collider other)
    {
        RunningSFXGob.SetActive(false);
        MovementStuff.SetActive(false);
        colloGob.SetActive(false);

        camTarget.parent = newCamTargetParent;
        camTarget.localPosition = Vector3.zero;
        camTarget.localRotation = Quaternion.identity;

        FinalAnimator.SetTrigger("Play");
    }
}
