using UnityEngine;

public class OneFuncToAnime : MonoBehaviour
{
    [SerializeField] private Animator anime;
    [SerializeField] private float cooldown = 1f;

    private float animeStartedAt;

    public void Trigger()
    {
        if (Time.time < animeStartedAt + cooldown) return;

        animeStartedAt = Time.time;
        anime.SetTrigger("Run");
    }
}