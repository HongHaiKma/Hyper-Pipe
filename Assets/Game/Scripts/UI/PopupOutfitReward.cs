using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfitReward : UICanvas
{
    public static int m_RandomEpicChar;
    // public bool int m_RandomEpicChar;
    public Image img_Char;
    public Button btn_AdsChar;
    public Button btn_NextLevel;

    public Coroutine coroutine;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT_REWARD;
        Init();
        GUIManager.Instance.AddClickEvent(btn_AdsChar, OnWatchAdsChar);
        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    public override void OnEnable()
    {
        coroutine = StartCoroutine(IENextLevelAppear());
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
        // m_RandomEpicChar = Random.Range(0, randomEpicChar.Count);

        int charId = randomEpicChar[Random.Range(0, randomEpicChar.Count)].m_Id;
        m_RandomEpicChar = charId;
        Helper.DebugLog("Char Epic: " + (CharacterType)charId);
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[charId - 1];
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
        // btn_AdsChar.gameObject.SetActive(false);
        // btn_NextLevel.gameObject.SetActive(false);
        // StartCoroutine(IEDelayClose());
    }

    IEnumerator DelayWinPopup()
    {
        // StopCoroutine(coroutine);
        btn_AdsChar.gameObject.SetActive(false);
        btn_NextLevel.gameObject.SetActive(false);
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
