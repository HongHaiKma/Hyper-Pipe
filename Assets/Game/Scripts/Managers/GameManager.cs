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
    public BigNumber m_GoldBeforeWin;

    public Material[] mat_Pipes;

    public bool m_StartLonger = false;

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
        ProfileManager.SetKeys(0);
        Helper.DebugLog("Reset keyssssssssssssssssss");
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.ADS_START_LONGER, Event_START_LONGER);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.ADS_START_LONGER, Event_START_LONGER);
    }

    public void Event_START_LONGER()
    {
        m_StartLonger = true;
        Helper.DebugLog("Start longer = " + m_StartLonger);
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

    // private void OnApplicationQuit()
    // {
    //     ProfileManager.SetKeys(0);
    //     Helper.DebugLog("OnApplicationQuit");
    //     Helper.DebugLog("Keys: " + ProfileManager.GetKeys());
    // }
}
