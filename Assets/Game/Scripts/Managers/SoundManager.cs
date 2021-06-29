using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_IngameShootingFx;
    public AudioSource m_BGM;

    public AudioClip m_ButtonClick;
    public AudioClip m_SoundGetGold;
    public AudioClip m_SoundObstacleDynamic;
    public AudioClip m_SoundBuySuccess;
    public AudioClip m_SoundWin;
    public AudioClip m_SoundWinLong;
    public AudioClip m_SoundLose;
    public AudioClip m_SoundCutPipe;
    public AudioClip m_ClipBGMInGame;



    public AudioClip m_WaterSpray;
    public AudioClip m_BGMTheme;

    public AudioClip m_SoundBigPrize;

    bool IsSoundOn
    {
        get
        {
            return GetSoundState() == 1;
        }
    }

    bool IsMusicOn
    {
        get
        {
            return GetMusicState() == 1;
        }
    }

    // private void Awake()
    // {
    //     m_BGM.Pause();
    // }

    // private void Start()
    // {
    //     m_BGM.Pause();
    // }

    public override void OnEnable()
    {
        base.OnEnable();
        OnSoundChange();
        OnMusicChange();
        // PlayBGM();


        StartListenToEvent();
    }

    public void StartListenToEvent()
    {
        // EventManager.AddListener(GameEvent.SOUND_CHANGE, OnSoundChange);
        // EventManager.AddListener(GameEvent.MUSIC_CHANGE, OnMusicChange);
        // EventManager.AddListener(GameEvent.CHAR_WIN, OnSoundWin);
        // EventManager.AddListener(GameEvent.CHAR_SPOTTED, OnSoundLose);
    }

    public void StopListenToEvent()
    {
        // EventManager.RemoveListener(GameEvent.SOUND_CHANGE, OnSoundChange);
        // EventManager.RemoveListener(GameEvent.MUSIC_CHANGE, OnMusicChange);
        // EventManager.RemoveListener(GameEvent.CHAR_WIN, OnSoundWin);
        // EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, OnSoundLose);
    }

    public void OnSoundChange()
    {
        if (IsSoundOn)
        {
            m_IngameShootingFx.volume = 0.3f;
        }
        else
        {
            m_IngameShootingFx.volume = 0f;
        }
    }

    public void OnMusicChange()
    {
        if (IsMusicOn)
        {
            m_BGM.volume = 0.6f;
            Helper.DebugLog("IsMusicOn");
        }
        else
        {
            m_BGM.volume = 0f;
        }
    }

    public void PlayBGM()
    {
        m_BGM.clip = m_BGMTheme;
        m_BGM.Play();
    }

    public void PlayWaterSpray()
    {
        m_BGM.clip = m_WaterSpray;
        m_BGM.Play();
    }

    public void PlayButtonClick()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_ButtonClick, 1);
        }
    }

    public void PlaySoundGetGold()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundGetGold, 5);
        }
    }

    public void PlaySoundObstacleDynamic(Vector3 pos)
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundObstacleDynamic, 2);
        }
    }

    public void PlaySoundBuySuccess()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundBuySuccess, 5);
        }
    }

    public void PlaySoundWin()
    {
        // // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundWin, 5);
        }
    }

    public void PlaySoundWinLong(bool _stop)
    {
        // if (_stop)
        // {
        //     m_IngameShootingFx.Stop();
        //     return;
        // }

        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundWinLong, 5);
        }
    }

    public void PlaySoundLose()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundLose, 5);
        }
    }

    public void PlaySoundCutPipe()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundCutPipe, 10);
        }
    }

    public void PlaySoundBigPrize()
    {
        // if (IsSoundOn && !IsLockSound)
        if (IsSoundOn)
        {
            m_IngameShootingFx.PlayOneShot(m_SoundBigPrize, 5);
        }
    }

    public void OnSoundWin()
    {
        m_BGM.Pause();
        PlaySoundWin();
    }

    public void OnSoundLose()
    {
        m_BGM.Pause();
        PlaySoundLose();
    }

    public void SetSoundState(int value)
    {
        PlayerPrefs.SetInt("Sound", value);
        // EventManager.CallEvent("MusicChange");
        OnSoundChange();
    }
    public void SetMusicState(int value)
    {
        PlayerPrefs.SetInt("Music", value);
        // EventManager.CallEvent("MusicChange");
        OnMusicChange();
    }

    public int GetSoundState()
    {
        return PlayerPrefs.GetInt("Sound", 1);
    }

    public int GetMusicState()
    {
        return PlayerPrefs.GetInt("Music", 1);
    }
}
