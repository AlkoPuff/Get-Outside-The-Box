using UnityEngine;

public class TwoFuncToAnime : MonoBehaviour
{



    [SerializeField] private Animator Anime;
    [SerializeField] private float Cooldown;
    [SerializeField] private float FuncsTimeIntervallum;
    private float AnimeStartedAt;

    private float Func1StartedAt;
    private float Func2StartedAt;

    public void Func1()
    {
        Func1StartedAt = Time.time;
        RunCheck();
    }

    public void Func2()
    {
        Func2StartedAt = Time.time;
        RunCheck();
    }


    private void RunCheck()
    {
        if (AnimeStartedAt + Cooldown > Time.time) return;

        if (Func1StartedAt + FuncsTimeIntervallum < Time.time) return;
        if (Func2StartedAt + FuncsTimeIntervallum < Time.time) return;

        AnimeStartedAt = Time.time;

        Anime.SetTrigger("Run");
    }
}
