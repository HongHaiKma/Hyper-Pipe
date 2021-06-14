using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusRewardCell : MonoBehaviour
{
    public Button btn_Claim;

    private void OnEnable()
    {
        btn_Claim.interactable = true;
    }
}
