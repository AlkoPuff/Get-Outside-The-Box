using UnityEngine;
using UnityEditor;

public class FindMesh : EditorWindow
{
    [MenuItem("Tools/Find Problematic MeshColliders")]
    static void Find()
    {
        int count = 0;

        MeshCollider[] colliders = Object.FindObjectsByType<MeshCollider>(FindObjectsSortMode.None);

        foreach (var mc in colliders)
        {
            var mesh = mc.sharedMesh;
            if (mesh == null)
                continue;

            bool convex = mc.convex;
            bool flat = IsMeshFlat(mesh);
            bool tiny = IsMeshTiny(mesh);

            if (convex && (flat || tiny))
            {
                Debug.LogWarning(
                    "Problematic MeshCollider found: " + mc.gameObject.name + "\n" +
                    "Mesh: " + mesh.name + "\n" +
                    "Issues: " +
                    (flat ? "Mesh is flat or nearly coplanar. " : "") +
                    (tiny ? "Mesh is extremely small. " : "") +
                    "Convex cooking may fail.",
                    mc.gameObject
                );

                Selection.activeObject = mc.gameObject;
                count++;
            }
        }

        if (count == 0)
            Debug.Log("No problematic MeshColliders found.");
        else
            Debug.Log("Finished. Found " + count + " problematic MeshColliders.");
    }

    static bool IsMeshFlat(Mesh mesh)
    {
        var bounds = mesh.bounds;
        float minThickness = Mathf.Min(bounds.size.x, Mathf.Min(bounds.size.y, bounds.size.z));
        return minThickness < 0.001f;
    }

    static bool IsMeshTiny(Mesh mesh)
    {
        var bounds = mesh.bounds;
        return bounds.size.magnitude < 0.01f;
    }
}
