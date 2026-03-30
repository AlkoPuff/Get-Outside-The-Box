using UnityEngine;
using System.Runtime.InteropServices;

public static class WebGLMouseRaw
{
    ////// AAAH OKAY I GIVE UP. SIMPLE INCREASED SENSITVITY FOR WEB.
    

    public static float WebSensitivityMultiplier = 3.2f;
    public static float WebSensitivityMultiplier_ForAxis = 5f;


    //#if UNITY_WEBGL && !UNITY_EDITOR
    //    [DllImport("__Internal")] private static extern void InitRawMouse();
    //    [DllImport("__Internal")] private static extern void ResetRawMouseDelta();
    //    [DllImport("__Internal")] private static extern float GetRawMouseDeltaX();
    //    [DllImport("__Internal")] private static extern float GetRawMouseDeltaY();
    //#endif

    //    private static Vector2 cachedDelta;
    //    private static int lastFrame = -1;

    //    // Call once at Start
    //    public static void Init()
    //    {
    //#if UNITY_WEBGL && !UNITY_EDITOR
    //        InitRawMouse();
    //#endif
    //    }

    //    // Internal: get delta once per frame
    //    private static Vector2 GetDeltaInternal()
    //    {
    //#if UNITY_WEBGL && !UNITY_EDITOR
    //        if (Time.frameCount != lastFrame)
    //        {
    //            float dx = GetRawMouseDeltaX();
    //            float dy = GetRawMouseDeltaY();

    //            cachedDelta = new Vector2(dx, dy);

    //            ResetRawMouseDelta(); // only once per frame
    //            lastFrame = Time.frameCount;
    //        }

    //        return cachedDelta;
    //#else
    //        return Input.mousePositionDelta;
    //#endif
    //    }

    //    public static Vector2 GetMousePositionDelta()
    //    {
    //#if UNITY_WEBGL && !UNITY_EDITOR
    //        return GetDeltaInternal();
    //#else
    //        return Input.mousePositionDelta;
    //#endif
    //    }
}