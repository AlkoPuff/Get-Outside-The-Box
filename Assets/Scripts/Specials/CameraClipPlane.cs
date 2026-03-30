using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class CameraClipPlane : MonoBehaviour
{
    public Transform clipPlane;

    Camera cam;

    void OnEnable()
    {
        cam = GetComponent<Camera>();
        RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
    }

    void BeginCameraRendering(ScriptableRenderContext context, Camera renderingCamera)
    {
        if (renderingCamera != cam || clipPlane == null)
            return;

        cam.ResetProjectionMatrix();

        Vector3 normal = clipPlane.forward;
        Vector3 pos = clipPlane.position;

        float side = Mathf.Sign(Vector3.Dot(normal, pos - cam.transform.position));
        normal *= side;

        Vector4 plane = CameraSpacePlane(cam, pos, normal);

        cam.projectionMatrix = cam.CalculateObliqueMatrix(plane);
    }

    Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal)
    {
        Matrix4x4 m = cam.worldToCameraMatrix;

        Vector3 cPos = m.MultiplyPoint(pos);
        Vector3 cNormal = m.MultiplyVector(normal).normalized;

        float d = -Vector3.Dot(cPos, cNormal);

        return new Vector4(cNormal.x, cNormal.y, cNormal.z, d);
    }
}