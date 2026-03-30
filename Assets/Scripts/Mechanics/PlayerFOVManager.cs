using System.Collections;
using UnityEngine;

public class PlayerFOVManager : MonoBehaviour
{
    public static PlayerFOVManager Instance;

    [SerializeField] private float fovLerpSpeed = 20f;

    private Camera[] allCams;
    private float targetFOV;

    private void Awake()
    {
        Instance = this;

        allCams = FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        targetFOV = Camera.main.fieldOfView;
    }

    public void AddToFOV(float amount, float time)
    {
        targetFOV += amount;
        StartCoroutine(ReturnFOV(amount, time));
    }

    private IEnumerator ReturnFOV(float amount, float time)
    {
        yield return new WaitForSeconds(time);
        targetFOV -= amount;
    }

    private void Update()
    {
        HandleFOV();
    }

    private void HandleFOV()
    {
        foreach (Camera cam in allCams)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        }
    }
}
