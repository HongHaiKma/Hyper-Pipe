using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[DefaultExecutionOrder(-90)]
public class PlaySceneManager : Singleton<PlaySceneManager>
{
    public GameObject g_Map;
    public TouchTrackPad m_TouchTrackPad;
    public GameObject g_JoystickTrackPad;
    public TouchJoystick m_JoystickTrackPad;

    public Button btn_Outfit;

    public Button btn_TestAds;

    public Button btn_MoveTruck;

    public TextMeshProUGUI txt_TotalGold;
    public TextMeshProUGUI txt_Level;
    public TextMeshProUGUI txt_Pipe;

    public GameObject g_Hand;

    public GameObject g_Keyss;
    public GameObject[] g_Keys;

    public Button btn_StartLonger;

    [Header("Setting")]
    public Button btn_Setting;
    public GameObject g_Setting;
    public Button btn_Sound;
    public Button btn_Music;
    public Button btn_Vibrate;
    public Button btn_Replay;
    public Image img_Sound;
    public Image img_Music;
    public Image img_Vibrate;


    [Header("Level String")]
    public GameObject g_LevelString;
    public LevelStringCell[] m_LevelStringCells;

    public GameObject g_LoadingAds;


    [Header("Test")]
    public Button btn_LoadMapTest;
    public Button btn_AddPipeTest;
    public Button btn_OpenBonusReward;
    public Button btn_AddKey;
    public Button btn_JumpLevel;
    public Button btn_Inter;
    public GameObject g_Block;

    private void Awake()
    {
        StartCoroutine(Blocking());
        AdsManager.Instance.m_WatchInter = true;

        GUIManager.Instance.AddClickEvent(btn_Outfit, OpenOutfitPopup);
        GUIManager.Instance.AddClickEvent(btn_OpenBonusReward, OpenBonusRewardPopup);
        GUIManager.Instance.AddClickEvent(btn_AddKey, AddKey);
        GUIManager.Instance.AddClickEvent(btn_JumpLevel, JumpLevel);
        GUIManager.Instance.AddClickEvent(btn_StartLonger, OnStartLonger);

        GUIManager.Instance.AddClickEvent(btn_MoveTruck, InGameObjectsManager.Instance.StartMoveTruck);

        GUIManager.Instance.AddClickEvent(btn_Setting, OnOpenSetting);
        GUIManager.Instance.AddClickEvent(btn_Sound, OnSetSound);
        GUIManager.Instance.AddClickEvent(btn_Music, OnSetMusic);
        GUIManager.Instance.AddClickEvent(btn_Vibrate, OnSetVibrate);
        GUIManager.Instance.AddClickEvent(btn_Replay, Replay);

        GUIManager.Instance.AddClickEvent(btn_Inter, TestAds);
        // GUIManager.Instance.AddClickEvent(btn_TestAds, TestAds);
    }

    public override void OnEnable()
    {
        txt_TotalGold.text = ProfileManager.GetGold();
        txt_Level.text = "Level " + ProfileManager.GetLevel().ToString();
        txt_Pipe.text = 0.ToString() + "m";
        // btn_StartLonger.gameObject.SetActive(false);
        SetupLevelString();
        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        EventManager.AddListener(GameEvent.LOAD_MAP, Event_LOAD_MAP);
        EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager.AddListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        // EventManager1<bool>.AddListener(GameEvent.GAME_START, Event_GAME_START);
    }

    public override void StopListenToEvents()
    {
        EventManager.RemoveListener(GameEvent.LOAD_MAP, Event_LOAD_MAP);
        EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager.RemoveListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        // EventManager1<bool>.AddListener(GameEvent.GAME_START, Event_GAME_START);
    }

    IEnumerator Blocking()
    {
        g_Block.SetActive(true);
        yield return Yielders.Get(0.2f);
        g_Block.SetActive(false);
    }

    public void OnOpenSetting()
    {
        g_Setting.SetActive(!g_Setting.activeInHierarchy);
        SetSoundImage();
        SetMusicImage();
        SetVibrateImage();
    }

    public void OnSetVibrate()
    {
        int vibrateOn = GameManager.Instance.GetVibrateState();
        if (vibrateOn == 1)
        {
            img_Vibrate.sprite = SpriteManager.Instance.m_Settings[4];
            GameManager.Instance.SetVibrateState(0);
        }
        else
        {
            img_Vibrate.sprite = SpriteManager.Instance.m_Settings[5];
            GameManager.Instance.SetVibrateState(1);
        }
    }

    public void SetVibrateImage()
    {
        int vibrateOn = GameManager.Instance.GetVibrateState();
        if (vibrateOn == 1)
        {
            img_Vibrate.sprite = SpriteManager.Instance.m_Settings[5];
        }
        else
        {
            img_Vibrate.sprite = SpriteManager.Instance.m_Settings[4];
        }
    }

    public void OnSetSound()
    {
        int soundOn = SoundManager.Instance.GetSoundState();
        if (soundOn == 1)
        {
            img_Sound.sprite = SpriteManager.Instance.m_Settings[0];
            SoundManager.Instance.SetSoundState(0);
        }
        else
        {
            img_Sound.sprite = SpriteManager.Instance.m_Settings[1];
            SoundManager.Instance.SetSoundState(1);
        }
    }

    public void SetSoundImage()
    {
        int soundOn = SoundManager.Instance.GetSoundState();
        if (soundOn == 1)
        {
            img_Sound.sprite = SpriteManager.Instance.m_Settings[1];
        }
        else
        {
            img_Sound.sprite = SpriteManager.Instance.m_Settings[0];
        }
    }

    public void OnSetMusic()
    {
        int musicOn = SoundManager.Instance.GetMusicState();
        if (musicOn == 1)
        {
            img_Music.sprite = SpriteManager.Instance.m_Settings[2];
            SoundManager.Instance.SetMusicState(0);
        }
        else
        {
            img_Music.sprite = SpriteManager.Instance.m_Settings[3];
            SoundManager.Instance.SetMusicState(1);
        }
    }

    public void SetMusicImage()
    {
        int musicOn = SoundManager.Instance.GetMusicState();
        if (musicOn == 1)
        {
            img_Music.sprite = SpriteManager.Instance.m_Settings[3];
        }
        else
        {
            img_Music.sprite = SpriteManager.Instance.m_Settings[2];
        }
    }

    public void SetupLevelString()
    {
        g_LevelString.SetActive(true);

        int factor = (ProfileManager.GetLevel() / 5);
        int odd = (ProfileManager.GetLevel() % 5);

        if (factor != 0 && odd == 0)
        {
            factor--;
        }

        for (int i = 0; i < m_LevelStringCells.Length; i++)
        {
            m_LevelStringCells[i].SetupCell(factor * 5 + i);
        }
    }

    public void Event_UPDATE_GOLD()
    {
        txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void Event_LOAD_MAP()
    {
        // m_TouchTrackPad.gameObject.SetActive(true);
        // btn_Outfit.gameObject.SetActive(false);
    }

    public void Event_END_GAME()
    {
        m_TouchTrackPad.gameObject.SetActive(false);
    }

    public void Event_GAME_START(bool _value)
    {
        btn_Outfit.gameObject.SetActive(_value);
    }

    public void Event_ADD_KEY()
    {
        BigNumber totalKeys = ProfileManager.GetKeys();
        for (int i = 0; i < g_Keys.Length; i++)
        {
            if (i < totalKeys)
            {
                g_Keys[i].SetActive(true);
            }
            else
            {
                g_Keys[i].SetActive(false);
            }
        }
        g_Keyss.transform.DOKill();
        g_Keyss.transform.DOLocalMoveX(-400f, 0.5f).OnComplete(
            () => g_Keyss.transform.DOLocalMoveX(-625f, 0.5f).SetDelay(1.5f)
        );
    }

    public void Event_ADD_PIPE(int _value)
    {
        txt_Pipe.text = _value.ToString() + "m";
        txt_Pipe.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).OnComplete(
            () => txt_Pipe.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f)
        );
    }

    public void OnStartLonger()
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.START_LONGER);
    }

    public void OpenOutfitPopup()
    {
        PopupCaller.OpenOutfitPopup();
    }

    public void OpenBonusRewardPopup()
    {
        PopupCaller.OpenBonusRewardPopup();
    }

    public void AddKey()
    {
        ProfileManager.AddKeys(1);
    }

    public void JumpLevel()
    {
        ProfileManager.Instance.PassOnlyLevel();
        GUIManager.Instance.LoadPlayScene();
    }

    public void TestAds()
    {
        AdsManager.Instance.WatchInterstitial();
    }

    public void Replay()
    {
        GUIManager.Instance.LoadPlayScene();
    }
}
