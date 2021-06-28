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
            // PopupCaller.OpenOutfitRewardPopup();
            // ProfileManager.UnlockNewCharacter(5);
            // ProfileManager.UnlockNewCharacter(8);
            // ProfileManager.UnlockNewCharacter(9);
            // ProfileManager.UnlockNewCharacter(11);
            // ProfileManager.UnlockNewCharacter(18);
            // ProfileManager.UnlockNewCharacter(20);
            PopupCaller.OpenOutfitRewardPopup();
        }
    }
}
