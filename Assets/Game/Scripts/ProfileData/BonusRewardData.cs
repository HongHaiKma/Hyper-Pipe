using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardData
{

}

public struct BonusRewardConfig
{
    public int m_Slot;
    public BigNumber m_Gold;

    public void Init(int _slot, BigNumber _gold)
    {
        m_Slot = _slot;
        m_Gold = _gold;
    }
}
