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

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT_REWARD;
        Init();
        GUIManager.Instance.AddClickEvent(btn_AdsChar, OnWatchAdsChar);
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
    }

    public void RandomEpicCharacter()
    {
        List<CharacterDataConfig> randomEpicChar = GameData.Instance.GetEpicCharacterDataConfig();
        for (int i = 0; i < randomEpicChar.Count; i++)
        {
            Helper.DebugLog("Char choose: " + randomEpicChar[i].m_Name);
        }
        m_RandomEpicChar = Random.Range(0, randomEpicChar.Count);
        Helper.DebugLog("Random Index List: " + m_RandomEpicChar);
        Helper.DebugLog("Random Char: " + randomEpicChar[m_RandomEpicChar].m_Name.ToString());

        int charId = randomEpicChar[m_RandomEpicChar].m_Id;
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[charId - 1];
        MiniCharacter.Instance.SpawnMiniCharacter(charId - 1);
        ProfileManager.UnlockEpicNewCharacter(charId);
        ProfileManager.SetSelectedCharacter(charId);
    }

    IEnumerator IENextLevelAppear()
    {
        yield return Yielders.Get(2f);
        btn_NextLevel.gameObject.SetActive(true);
    }

    public override void OnClose()
    {
        base.OnClose();
        PopupCaller.OpenWinPopup(false, false);
        EventManager.CallEvent(GameEvent.POPUP_WIN_BUTTON_APPEAR);
    }
}
