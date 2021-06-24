using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Helper.DebugLog("Gold: " + (GameManager.Instance.m_GoldWin * 3));
            Helper.DebugLog("Gold Profile: " + ProfileManager.GetGold().ToString());
        }
    }
}
