using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBonusReward : UICanvas
{
    public List<BonusRewardCell> m_BonusCells = new List<BonusRewardCell>();

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
}
