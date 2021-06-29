using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfitReward : UICanvas
{
    public static int m_RandomEpicChar;
    public Image img_Char;
    public Button btn_AdsChar;
    public Button btn_NextLevel;

    public GameObject g_LoseIt;

    public Coroutine coroutine;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT_REWARD;
        Init();

        GUIManager.Instance.AddClickEvent(btn_AdsChar, OnWatchAdsChar);
        GUIManager.Instance.AddClickEvent(btn_NextLevel, OnClose2);
        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    public override void OnEnable()
    {
        StartCoroutine(IENextLevelAppear());
        RandomEpicCharacter();
        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.ADS_OUTFIT_PROGRESS_ANIM, OnClose);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.ADS_OUTFIT_PROGRESS_ANIM, OnClose);
    }

    public void OnWatchAdsChar()
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.OUTFIT_PROGRESS);
        // ProfileManager.UnlockEpicNewCharacter(PopupOutfitReward.m_RandomEpicChar);
        // ProfileManager.SetSelectedCharacter(PopupOutfitReward.m_RandomEpicChar);
        // OnClose();
    }

    public void RandomEpicCharacter()
    {
        List<CharacterDataConfig> randomEpicChar = GameData.Instance.GetEpicCharacterDataConfig();

        int charId = randomEpicChar[Random.Range(0, randomEpicChar.Count)].m_Id;
        m_RandomEpicChar = charId;
        MiniCharacter.Instance.SpawnMiniCharacter(charId - 1);
    }

    IEnumerator IENextLevelAppear()
    {
        yield return Yielders.Get(2f);
        btn_NextLevel.gameObject.SetActive(true);
    }

    public override void OnClose()
    {
        StartCoroutine(DelayWinPopup());
    }

    public void OnClose2()
    {
        base.OnClose();
        PopupCaller.OpenWinPopup(false, false);
        EventManager.CallEvent(GameEvent.POPUP_WIN_BUTTON_APPEAR);
    }

    IEnumerator DelayWinPopup()
    {
        btn_AdsChar.gameObject.SetActive(false);
        g_LoseIt.gameObject.SetActive(false);
        yield return Yielders.Get(2f);
        base.OnClose();
        PopupCaller.OpenWinPopup(false, false);
        EventManager.CallEvent(GameEvent.POPUP_WIN_BUTTON_APPEAR);
    }

    IEnumerator IEDelayClose()
    {
        yield return Yielders.Get(2f);
        PopupCaller.OpenWinPopup(false, false);
        EventManager.CallEvent(GameEvent.POPUP_WIN_BUTTON_APPEAR);
    }
}
