using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{

}

public struct LevelConfig
{
    public int m_Id;
    public BigNumber m_MaxLevel;
    public BigNumber m_MinGold;

    public void Init(int _id, BigNumber _maxLevel, BigNumber _minGold)
    {
        m_Id = _id;
        m_MaxLevel = _maxLevel;
        m_MinGold = _minGold;
    }

    public bool CheckInRange(BigNumber _level)
    {
        return (_level <= m_MaxLevel);
    }
}

public struct LevelEasyConfig
{
    public int m_Id;
    public int m_PathCell1;
    public int m_PathCell2;
    public int m_PathCell3;
    public int m_PathCell4;

    public void Init(int _id, int _pathCell1, int _pathCell2, int _pathCell3, int _pathCell4)
    {
        m_Id = _id;
        m_PathCell1 = _pathCell1;
        m_PathCell2 = _pathCell2;
        m_PathCell3 = _pathCell3;
        m_PathCell4 = _pathCell4;
    }
}