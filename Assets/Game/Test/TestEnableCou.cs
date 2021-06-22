using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnableCou : MonoBehaviour
{
    public GameObject g_TestCou;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            g_TestCou.SetActive(true);
        }
    }
}
