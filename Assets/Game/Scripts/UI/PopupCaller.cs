using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCaller
{
    public static void OpenOutfitPopup(bool _isClose = false)
    {
        PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;

        GUIManager.Instance.ShowUIPopup(popup, _isClose);
    }

    public static void OpenWinPopup(bool _isClose = false, bool _isSetup = true)
    {
        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;

        GUIManager.Instance.ShowUIPopup(popup, _isClose, _isSetup);
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

    public static void OpenOutfitRewardPopup()
    {
        PopupOutfitReward popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT_REWARD) as PopupOutfitReward;

        GUIManager.Instance.ShowUIPopup(popup);
    }
}
