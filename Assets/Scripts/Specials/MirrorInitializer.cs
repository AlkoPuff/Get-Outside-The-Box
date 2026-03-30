using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MirrorInitializer : MonoBehaviour
{
    [Header("Mirror Setup")]
    [SerializeField] private Camera MirrorCamera;
    [SerializeField] private Material MirrorMaterial_PREFAB;

    [SerializeField] private Renderer Renderer;

    private RenderTexture mirrorRT;
    private Material mirrorMaterialInstance;

    void Start()
    {
        InitializeMirror();
    }

    void InitializeMirror()
    {
        MirrorCamera.gameObject.SetActive(true);

        mirrorRT = new RenderTexture(ResolutionManager.Resolution.x, ResolutionManager.Resolution.y, 24, RenderTextureFormat.ARGB32)
        {
            name = "MirrorRT",
            useMipMap = false,
            autoGenerateMips = false,
            antiAliasing = 4
        };
        mirrorRT.Create();

        MirrorCamera.targetTexture = mirrorRT;

        mirrorMaterialInstance = new Material(MirrorMaterial_PREFAB)
        {
            name = "MirrorMaterial_Instance"
        };

        mirrorMaterialInstance.SetTexture("_Main", mirrorRT);

        Renderer.material = mirrorMaterialInstance;
    }

    void OnDestroy()
    {
        if (mirrorMaterialInstance != null)
        {
            Destroy(mirrorMaterialInstance);
            mirrorMaterialInstance = null;
        }

        if (mirrorRT != null)
        {
            mirrorRT.Release();
            Destroy(mirrorRT);
            mirrorRT = null;
        }
    }
}