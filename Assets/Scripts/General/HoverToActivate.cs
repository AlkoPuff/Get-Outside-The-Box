using UnityEngine;

public class HoverToActivate : Interactable
{

    [SerializeField] private GameObject Target;

    public override void OnHoverStarted()
    {
        if (!Target.activeSelf)
            Target.SetActive(true);
    }
    public override void OnHoverEnded()
    {
        if (Target.activeSelf)
            Target.SetActive(false);
    }
}
