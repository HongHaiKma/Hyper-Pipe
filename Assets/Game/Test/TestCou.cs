using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCou : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TestIE());
    }

    IEnumerator TestIE()
    {
        yield return Yielders.Get(2f);
        Helper.DebugLog("Testttttttttttttttttt");
    }
}
