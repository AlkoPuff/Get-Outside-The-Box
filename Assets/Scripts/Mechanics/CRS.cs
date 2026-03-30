using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRS : MonoBehaviour
{
    public static CRS s;
    private void Awake()
    {
        s = this;
    }

    public void StartC(IEnumerator co)
    {
        StartCoroutine(co);
    }
}
