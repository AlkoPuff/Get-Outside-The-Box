using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{

    [SerializeField] private GameObject TriggerParent;
    [SerializeField] private int MusicIndex;


    private void OnTriggerEnter(Collider other)
    {
        MusicManager.Instance.PlayMusic(MusicIndex);
        TriggerParent.SetActive(false);
    }
}
