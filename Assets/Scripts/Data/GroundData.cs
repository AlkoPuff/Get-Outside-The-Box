using System.Collections.Generic;
using UnityEngine;

public class GroundData : MonoBehaviour
{
    public static GroundData NoGroundData;
    public static GroundData DefaultGroundData;


    [Header("Data")]
    public bool isGrounded;
    public bool ignoreFriction;
    public bool thin;


    [Header("Statics")]
    public bool YouAreNoGroundData;
    public bool YouAreDefaultGroundData;


    private void Awake()
    {
        SetStatics();

        HandleThin();
    }

    private void SetStatics()
    {
        if (YouAreDefaultGroundData)
            DefaultGroundData = this;

        else if (YouAreNoGroundData)
            NoGroundData = this;
    }


    private static List<Collider> ThinColliders = new List<Collider>();
    private void HandleThin()
    {
        if (thin)
        {
            Collider[] colls = GetComponents<Collider>();
            foreach (Collider col in colls)
            {
                ThinColliders.Add(col);
            }
        }
    }

    private static bool ThinsEnabled = true;
    public static void DisableThins()
    {
        if (!ThinsEnabled) return;
        ThinsEnabled = false;

        foreach (Collider col in ThinColliders)
        {
            col.enabled = false;
        }
    }
    public static void EnableThins()
    {
        if (ThinsEnabled) return;
        ThinsEnabled = true;

        foreach (Collider col in ThinColliders)
        {
            col.enabled = true;
        }
    }
}
