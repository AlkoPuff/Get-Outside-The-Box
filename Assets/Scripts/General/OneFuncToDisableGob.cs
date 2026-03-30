using UnityEngine;
using System.Collections;

public class OneFuncToDisableGob : MonoBehaviour
{
    [SerializeField] private GameObject DisableGob;
    [SerializeField] private float DisableTime;

    public void DisableGobFunc()
    {
        if (!DisableGob.activeSelf) return;

        StartCoroutine(DisableForDuration());
    }

    private IEnumerator DisableForDuration()
    {
        DisableGob.SetActive(false);
        yield return new WaitForSeconds(DisableTime);
        DisableGob.SetActive(true);
    }
}
