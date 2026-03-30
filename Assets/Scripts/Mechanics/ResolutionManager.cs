using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public static int2 Resolution = new int2(640, 360);
    public static float WebMouseSensitivityMultiplier = 1.35f;
    void Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Screen.SetResolution(Resolution.x, Resolution.y, FullScreenMode.FullScreenWindow);
#endif
    }
}