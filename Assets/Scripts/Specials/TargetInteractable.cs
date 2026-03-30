using UnityEngine;
using UnityEngine.Events;

public class TargetInteractable : Interactable
{

    public UnityEvent TargetHitEvent;


    [SerializeField] private Animator TargetAnime;
    [SerializeField] private float Cooldown = 0.5f;

    private float LastTime = 0;
    public override void OnClick()
    {
        if (LastTime + Cooldown > Time.time) return;
        LastTime = Time.time;

        if (TargetAnime != null)
            TargetAnime.Play("Hit");

        TargetHitEvent?.Invoke();
    }

}
