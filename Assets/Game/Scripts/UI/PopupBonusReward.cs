using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBonusReward : UICanvas
{
    public List<BonusRewardCell> m_BonusCells = new List<BonusRewardCell>();
    public static int m_Char;
    public GameObject[] g_Keys;
    public GameObject g_Keyss;

    public Button btn_Ads3Keys;
    public Button btn_LoseIt;

    public Image img_Char;

    private void Awake()
    {
        m_ID = UIID.POPUP_BONUS_REWARD;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Ads3Keys, Watch3Keys);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Helper.DebugLog("POpup bonus reward on enableeeeeeeeeeeeeeeee");
        ResetPopup();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.POPUP_BONUS_REWARD_UPDATE, UpdateKeys);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.AddListener(GameEvent.POPUP_BONUS_REWARD_UPDATE, UpdateKeys);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // ProfileManager.UnlockNewCharacter(CharacterType.WORKER);
            // SetupRandom();

        }
    }

    public void ResetPopup()
    {
        SetupRandom();
        UpdateKeys();
        g_Keyss.SetActive(true);
        btn_Ads3Keys.gameObject.SetActive(false);
        btn_LoseIt.gameObject.SetActive(false);

        // for (int i = 0; i < m_BonusCells.Count; i++)
        // {
        //     m_BonusCells[i].Reset();
        // }
    }

    public void UpdateKeys()
    {
        if (ProfileManager.GetKeys() > 3)
        {
            ProfileManager.SetKeys(3);
        }

        for (int i = 0; i < g_Keys.Length; i++)
        {
            g_Keys[i].SetActive(false);
        }

        for (int i = 0; i < ProfileManager.GetKeys(); i++)
        {
            if (i >= 3)
            {
                break;
            }
            g_Keys[i].SetActive(true);
        }

        if (ProfileManager.GetKeys() <= 0)
        {
            StartCoroutine(IEOutOfKeys());
        }
    }

    IEnumerator IEOutOfKeys()
    {
        btn_Ads3Keys.gameObject.SetActive(true);
        g_Keyss.SetActive(false);
        yield return new WaitForSeconds(2f);
        btn_LoseIt.gameObject.SetActive(true);
    }

    public void SetupRandom()
    {
        List<BonusRewardCell> cells = new List<BonusRewardCell>();
        for (int i = 0; i < m_BonusCells.Count; i++)
        {
            cells.Add(m_BonusCells[i]);
        }

        List<CharacterDataConfig> config = GameData.Instance.GetLegendCharacterDataConfig();

        for (int i = 0; i < config.Count; i++)
        {
            if (ProfileManager.IsOwned(config[i].m_Id))
            {
                config.Remove(config[i]);
            }
        }

        int count = config.Count;
        int charId = 0;
        if (count > 0)
        {
            int random = Random.Range(0, count - 1);
            charId = GameData.Instance.GetCharacterDataConfig(config[random].m_Id).m_Id - 1;
            img_Char.sprite = SpriteManager.Instance.m_CharCards[charId];
            m_Char = charId + 1;
            Helper.DebugLog("m_Char = " + m_Char);
        }

        Dictionary<int, BonusRewardConfig> bonusLength = GameData.Instance.GetBonusRewardConfig();

        List<BonusRewardConfig> rewardConfigs = new List<BonusRewardConfig>();

        for (int i = 1; i <= bonusLength.Count; i++)
        {
            rewardConfigs.Add(bonusLength[i]);
        }

        for (int i = 0; i < cells.Count; i++)
        {
            int random = Random.Range(0, rewardConfigs.Count);
            BonusRewardConfig reward = rewardConfigs[random];
            cells[i].m_Gold = reward.m_Gold;
            cells[i].txt_Gold.text = reward.m_Gold.ToString();

            if (reward.m_Slot == 9 && count > 0)
            {
                cells[i].m_IsChar = true;
                cells[i].img_Char.sprite = SpriteManager.Instance.m_CharCards[charId];
            }
            else
            {
                cells[i].m_IsChar = false;
            }

            Helper.DebugLog(reward.m_Slot);

            rewardConfigs.Remove(reward);
        }
    }

    public void Watch3Keys()
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.KEYS3_1);
    }

    public override void OnClose()
    {
        base.OnClose();
        if (((ProfileManager.GetLevel() - 1) % 5 == 0) && (GameData.Instance.GetEpicCharacterDataConfig().Count > 0))
        {
            PopupCaller.OpenOutfitProgressPopup();
        }
        else
        {
            PopupCaller.OpenWinPopup();
        }
    }
}
