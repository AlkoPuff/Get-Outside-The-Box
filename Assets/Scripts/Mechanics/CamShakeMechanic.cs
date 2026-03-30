using System.Collections;
using UnityEngine;

public class CamShakeMechanic : MonoBehaviour
{
    public static CamShakeMechanic Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Animator CamAnime;


    float TotalAmp = 0f;
    public void AddShake(float Amp, float time)
    {
        TotalAmp += Amp;
        CamAnime.SetFloat("Blend", TotalAmp);

        StartCoroutine(RemoveShake(Amp, time));
    }

    IEnumerator RemoveShake(float Amp, float Time)
    {
        yield return new WaitForSeconds(Time);
        TotalAmp -= Amp;
        CamAnime.SetFloat("Blend", TotalAmp);
    }
}
