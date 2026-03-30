using UnityEngine;

public class PlanarReflection : MonoBehaviour
{
    [SerializeField] private Transform ReflectionCamera;
    [SerializeField] private Transform OverrideMainCam;

    Transform MirrorTR;
    Transform MainCamTR;

    private void LateUpdate()
    {
        if (MainCamTR == null)
            MainCamTR = Camera.main.transform;

        if (OverrideMainCam != null)
            MainCamTR = OverrideMainCam;

        if (MirrorTR == null)
            MirrorTR = transform;

        Vector3 normal = MirrorTR.forward;

        Vector3 toCam = MainCamTR.position - MirrorTR.position;

        Vector3 reflectedPos = Vector3.Reflect(toCam, normal);

        ReflectionCamera.position = MirrorTR.position + reflectedPos;

        Vector3 reflectedForward = Vector3.Reflect(MainCamTR.forward, normal);

        Vector3 reflectedUp = Vector3.Reflect(MainCamTR.up, normal);

        Vector3 right = Vector3.Cross(reflectedUp, reflectedForward).normalized;
        reflectedUp = Vector3.Cross(reflectedForward, right);

        ReflectionCamera.rotation = Quaternion.LookRotation(reflectedForward, reflectedUp);

    }
}