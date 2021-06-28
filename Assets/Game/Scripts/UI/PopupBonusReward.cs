using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopupBonusReward : UICanvas
{
    public List<BonusRewardCell> m_BonusCells = new List<BonusRewardCell>();
    public static int m_Char;
    public GameObject[] g_Keys;
    public GameObject g_Keyss;

    public TextMeshProUGUI txt_KeyCount;

    public Button btn_Ads3Keys;
    public Button btn_LoseIt;
    public Button btn_NextLevel;

    public Image img_Char;

    public bool m_OpenAgain;

    [Header("Best Prize")]
    public GameObject g_CharRender;
    public GameObject g_GoldBest;

    private void Awake()
    {
        m_ID = UIID.POPUP_BONUS_REWARD;
        Init();

        m_OpenAgain = false;
        GUIManager.Instance.AddClickEvent(btn_Ads3Keys, Watch3Keys);
        GUIManager.Instance.AddClickEvent(btn_NextLevel, OnClose);

        btn_NextLevel.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        ResetPopup();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        // EventManager.AddListener(GameEvent.POPUP_BONUS_REWARD_UPDATE, UpdateKeys);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        // EventManager.AddListener(GameEvent.POPUP_BONUS_REWARD_UPDATE, UpdateKeys);
    }

    public override void Setup()
    {
        base.Setup();
        ResetPopup();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         // ProfileManager.UnlockNewCharacter(CharacterType.WORKER);
    //         // SetupRandom();

    //     }
    // }

    public void ResetPopup()
    {
        for (int i = 0; i < m_BonusCells.Count; i++)
        {
            m_BonusCells[i].Reset();
        }
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
        if (!m_OpenAgain)
        {
            if (ProfileManager.GetKeys() > 3)
            {
                ProfileManager.SetKeys(3);
            }
        }
        else
        {
            if (ProfileManager.GetKeys() > 6)
            {
                ProfileManager.SetKeys(6);
            }
        }

        g_Keyss.transform.DOScale(1.5f, 0.25f).OnComplete(
            () =>
            {
                txt_KeyCount.text = "X" + ProfileManager.GetKeys().ToString();
                g_Keyss.transform.DOScale(1f, 0.25f).OnComplete(
                    () =>
                    {
                        if (ProfileManager.GetKeys() <= 0)
                        {
                            if (m_OpenAgain)
                            {
                                btn_NextLevel.gameObject.SetActive(true);
                                btn_Ads3Keys.gameObject.SetActive(false);
                                btn_LoseIt.gameObject.SetActive(false);
                                g_Keyss.SetActive(false);
                            }
                            else
                            {
                                StartCoroutine(IEOutOfKeys());
                            }
                        }
                    }
                );
            }
        );

        // for (int i = 0; i < g_Keys.Length; i++)
        // {
        //     g_Keys[i].SetActive(false);
        // }

        // for (int i = 0; i < ProfileManager.GetKeys(); i++)
        // {
        //     if (i >= 3)
        //     {
        //         break;
        //     }
        //     g_Keys[i].SetActive(true);
        // }

        // if (ProfileManager.GetKeys() <= 0)
        // {
        //     if (m_OpenAgain)
        //     {
        //         btn_NextLevel.gameObject.SetActive(true);
        //         btn_Ads3Keys.gameObject.SetActive(false);
        //         btn_LoseIt.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         StartCoroutine(IEOutOfKeys());
        //     }
        // }
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
            g_CharRender.SetActive(true);
            g_GoldBest.SetActive(false);
            int random = Random.Range(0, count - 1);
            charId = GameData.Instance.GetCharacterDataConfig(config[random].m_Id).m_Id - 1;
            MiniCharacter.Instance.SpawnMiniCharacter(charId);
            // img_Char.sprite = SpriteManager.Instance.m_CharCards[charId];
            m_Char = charId + 1;
            Helper.DebugLog("m_Char = " + m_Char);
        }
        else
        {
            g_CharRender.SetActive(false);
            g_GoldBest.SetActive(true);
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
        btn_LoseIt.gameObject.SetActive(false);
        AdsManager.Instance.WatchRewardVideo(RewardType.KEYS3_1);
        // PopupCaller.OpenBonusRewardPopup();
    }

    public override void OnClose()
    {
        base.OnClose();
        // if (((ProfileManager.GetLevel() - 1) % 5 == 0) && (GameData.Instance.GetEpicCharacterDataConfig().Count > 0))
        // {
        //     PopupCaller.OpenOutfitRewardPopup();
        // }
        // else
        // {
        PopupCaller.OpenWinPopup();
        // }
    }
}
