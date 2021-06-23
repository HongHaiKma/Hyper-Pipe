using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;
using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public int m_ScoreFactor;
    public List<Color> m_ScoreLineColor;
    public int m_KeyInGameStep;
    public BigNumber m_GoldWin;

    public Material[] mat_Pipes;

    bool IsVibrateOn
    {
        get
        {
            return GetVibrateState() == 1;
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Vibrate(int _type)
    {
        if (IsVibrateOn)
        {
            if (_type == 0)
            {
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            }
            if (_type == 1)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            }
        }
    }

    public int GetVibrateState()
    {
        return PlayerPrefs.GetInt("Vibrate", 1);
    }

    public void SetVibrateState(int value)
    {
        PlayerPrefs.SetInt("Vibrate", value);
    }

    public void StopAllVibrates()
    {
        MMVibrationManager.StopAllHaptics(true);
    }
}
