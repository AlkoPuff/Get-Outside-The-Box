using UnityEngine;

public class GlobalMouseManager : MonoBehaviour
{
    public static GlobalMouseManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public static float GlobalSensitivityMultiplier = 1f;

    public void OnClick()
    {

    }
    public void OnRelease()
    {

    }

    public void ValChange()
    {
        GlobalSensitivityMultiplier = PositionQuadCollScr.YouAreMouseSensitivity.CurrentValue;
    }
}
