using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopupWin : UICanvas
{
    public Button btn_AdsGold;
    public Button btn_NextLevel;
    public TextMeshProUGUI txt_GoldWin;
    public TextMeshProUGUI txt_AdsGold;

    public TextMeshProUGUI txt_TotalGold;
    public GameObject g_TotalGold;

    public Image img_GiftFill;
    public TextMeshProUGUI txt_Percent;

    public GameObject g_Effect;
    public Transform tf_EffectParent;

    [Header("Random Epic Char")]
    public static int m_RandomEpicChar;
    public Image img_Char;

    public Transform tf_EndGold;
    public Transform tf_StartGoldClaim;
    public Transform tf_StartGoldAds;

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        GUIManager.Instance.AddClickEvent(btn_NextLevel, OnNextLevel);
        GUIManager.Instance.AddClickEvent(btn_AdsGold, WatchAdsGold);


        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.ADS_GOLD_2_ANIM, SpawnGoldEffectFromAds);
        EventManager.AddListener(GameEvent.POPUP_WIN_BUTTON_APPEAR, DelayForButtonAppear);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.ADS_GOLD_2_ANIM, SpawnGoldEffectFromAds);
        EventManager.RemoveListener(GameEvent.POPUP_WIN_BUTTON_APPEAR, DelayForButtonAppear);
    }

    public override void Setup()
    {
        base.Setup();
        txt_Percent.text = "0%";
        img_GiftFill.fillAmount = 0f;
        btn_AdsGold.interactable = false;
        float aaa = ((ProfileManager.GetLevel() - 1) % 5f) / 5f;
        if (aaa == 0f)
        {
            aaa = 1;
        }
        else
        {
            g_Effect.SetActive(false);
        }
        img_GiftFill.DOFillAmount(aaa, aaa * 1.5f).OnStart(
            () =>
            {
                txt_Percent.text = aaa * 100f + "%";
            }
        ).OnComplete(
            () =>
            {
                if (aaa == 1)
                {
                    g_Effect.SetActive(true);
                }
            }
        );

        btn_NextLevel.gameObject.SetActive(false);
        StartCoroutine(IEDelayForOutfitRewardPopup());

        txt_GoldWin.text = (GameManager.Instance.m_GoldWin / 2).ToCharacterFormat();
        txt_AdsGold.text = (GameManager.Instance.m_GoldWin * 3 + (GameManager.Instance.m_GoldWin / 2)).ToCharacterFormat();
        txt_TotalGold.text = GameManager.Instance.m_GoldBeforeWin.ToString();
    }

    public void RandomEpicCharacter()
    {
        List<CharacterDataConfig> randomEpicChar = GameData.Instance.GetEpicCharacterDataConfig();
        m_RandomEpicChar = Random.Range(0, randomEpicChar.Count);

        int charId = randomEpicChar[m_RandomEpicChar].m_Id;
        img_Char.sprite = SpriteManager.Instance.m_CharCards[charId - 1];
        ProfileManager.UnlockEpicNewCharacter(charId);
    }

    public void OnNextLevel()
    {
        SpawnGoldEffectFromClaim();
    }

    public void WatchAdsGold()
    {
        AdsManager.Instance.WatchRewardVideo(RewardType.GOLD_2);
        // ProfileManager.AddGold(GameManager.Instance.m_GoldWin * 3);
        // SpawnGoldEffectFromAds();
    }

    public void SpawnGoldEffectFromAds()
    {
        btn_AdsGold.interactable = false;
        btn_NextLevel.interactable = false;
        SpawnGoldEffect(tf_StartGoldAds.position, (GameManager.Instance.m_GoldWin * 3) / 15);
    }

    public void SpawnGoldEffectFromClaim()
    {
        btn_AdsGold.interactable = false;
        btn_NextLevel.interactable = false;
        SpawnGoldEffect(tf_StartGoldClaim.position, (GameManager.Instance.m_GoldWin / 2) / 15);
    }

    public void SpawnGoldEffect(Vector3 _pos, BigNumber _goldAdd)
    {
        InGameObjectsManager.Instance.DespawnGoldEffectPool();
        // txt_AdsGold.text = ProfileManager.GetGold().ToString();
        // BigNumber _goldAdd = (GameManager.Instance.m_GoldWin / 2) / 15;

        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnGoldEffect(ConfigKeys.m_GoldEffect1, _pos);
            g_EffectGold.transform.SetParent(tf_EffectParent);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);

            InGameObjectsManager.Instance.g_GoldEffects.Add(g_EffectGold);

            g_EffectGold.transform.DOKill();

            g_EffectGold.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            g_EffectGold.transform.DOScale(1f, 0.7f).SetDelay(0.1f + i * 0.1f);
            g_EffectGold.transform.DOMove(tf_EndGold.position, 0.7f).SetDelay(0.1f + i * 0.1f).OnComplete(
                () =>
                {
                    PrefabManager.Instance.DespawnPool(g_EffectGold);
                    txt_TotalGold.transform.DOScale(1.3f, 0.3f).OnStart(
                        () =>
                        {
                            BigNumber goldAdd1 = (GameManager.Instance.m_GoldBeforeWin += _goldAdd).RoundToInt();
                            txt_TotalGold.text = goldAdd1.ToCharacterFormat();
                        }
                    ).OnComplete(
                        () =>
                        {
                            txt_TotalGold.transform.DOScale(1f, 0.1f);
                            SoundManager.Instance.PlaySoundGetGold();
                        }
                    );
                }
            );
        }

        StartCoroutine(IELoadScene());
    }

    IEnumerator IELoadScene()
    {
        yield return Yielders.Get(2.5f);
        GUIManager.Instance.LoadPlayScene(true);
    }

    IEnumerator IEDelayForOutfitRewardPopup()
    {
        yield return Yielders.Get(2f);

        float aaa = ((ProfileManager.GetLevel() - 1) % 5f) / 5f;
        if (aaa == 0f)
        {
            PopupCaller.OpenOutfitRewardPopup();
        }
        else
        {
            ButtonAppear();
        }
    }

    public void DelayForButtonAppear()
    {
        StartCoroutine(IEDelayForButtonAppear());
    }

    IEnumerator IEDelayForButtonAppear()
    {
        yield return Yielders.Get(2f);
        ButtonAppear();
    }

    public void ButtonAppear()
    {
        btn_NextLevel.gameObject.SetActive(true);
        btn_NextLevel.interactable = true;
        btn_AdsGold.interactable = true;
    }
}
