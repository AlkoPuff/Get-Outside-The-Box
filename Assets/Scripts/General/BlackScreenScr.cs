using UnityEngine;

public class BlackScreenScr : MonoBehaviour
{

    [SerializeField] private GameObject FinalUIGob;
    [SerializeField] private Animator FinalUIAnime;

    private void OnEnable()
    {
        FinalUIGob.SetActive(true);
        FinalUIAnime.SetTrigger("Play");
    }
}
