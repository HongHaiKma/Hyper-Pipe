using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusRewardCell : MonoBehaviour
{
    public Button btn_Claim;
    public GameObject g_Gold;
    public GameObject g_Char;
    public GameObject g_Chest;
    public TextMeshProUGUI txt_Gold;
    public Image img_Char;
    public BigNumber m_Gold = new BigNumber(0);
    public bool m_IsChar;

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_Claim, Claim);
    }

    private void OnEnable()
    {
        btn_Claim.interactable = true;
        g_Chest.SetActive(true);
        g_Char.SetActive(false);
        g_Gold.SetActive(false);
    }

    public void Claim()
    {
        if (m_IsChar)
        {
            g_Char.SetActive(true);
            g_Gold.SetActive(false);
            ProfileManager.UnlockNewCharacter(PopupBonusReward.m_Char);
        }
        else
        {
            g_Char.SetActive(false);
            g_Gold.SetActive(true);
            ProfileManager.AddGold(m_Gold);
        }

        g_Chest.SetActive(false);
        ProfileManager.AddKeys(-1);
    }
}
