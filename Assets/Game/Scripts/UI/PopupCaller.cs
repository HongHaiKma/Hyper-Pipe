using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCaller
{
    public static void OpenOutfitPopup()
    {
        PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenWinPopup()
    {
        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenBonusRewardPopup(bool _isClose = false)
    {
        PopupBonusReward popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_BONUS_REWARD) as PopupBonusReward;

        GUIManager.Instance.ShowUIPopup(popup, _isClose);
    }

    public static PopupBonusReward GetBonusRewardPopup()
    {
        PopupBonusReward popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_BONUS_REWARD) as PopupBonusReward;

        return popup;
    }
}
