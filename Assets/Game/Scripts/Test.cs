using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // PopupCaller.OpenWinPopup();
            // ProfileManager.AddGold(1499);
            PopupCaller.OpenOutfitRewardPopup();
        }
    }
}
