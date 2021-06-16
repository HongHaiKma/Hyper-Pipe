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

    [Header("Random Epic Char")]
    public static int m_RandomEpicChar;
    public Image img_Char;

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
        btn_AdsGold.interactable = true;
        btn_NextLevel.interactable = true;

        btn_NextLevel.gameObject.SetActive(false);
        StartCoroutine(IENextLevelAppear());

        txt_GoldWin.text = GameManager.Instance.m_GoldWin.ToString();
        txt_AdsGold.text = (GameManager.Instance.m_GoldWin * 3).ToString();

        // RandomEpicCharacter();

        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.ADS_GOLD_2_ANIM, SpawnGoldEffectFromAds);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.ADS_GOLD_2_ANIM, SpawnGoldEffectFromAds);
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
        // SpawnGoldEffectFromAds();
    }

    public void SpawnGoldEffectFromAds()
    {
        btn_AdsGold.interactable = false;
        btn_NextLevel.interactable = false;
        SpawnGoldEffect(btn_AdsGold.gameObject.transform.position);
    }

    public void SpawnGoldEffectFromClaim()
    {
        btn_AdsGold.interactable = false;
        btn_NextLevel.interactable = false;
        SpawnGoldEffect(btn_NextLevel.gameObject.transform.localPosition);
    }

    public void SpawnGoldEffect(Vector3 _pos)
    {
        InGameObjectsManager.Instance.DespawnGoldEffectPool();

        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnGoldEffect(ConfigKeys.m_GoldEffect1, _pos);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = transform.position;

            InGameObjectsManager.Instance.g_GoldEffects.Add(g_EffectGold);

            g_EffectGold.transform.DOKill();

            g_EffectGold.transform.DOMove(PlaySceneManager.Instance.txt_TotalGold.gameObject.transform.position, 0.7f).SetDelay(0.1f + i * 0.1f).OnComplete(
                () =>
                {
                    PrefabManager.Instance.DespawnPool(g_EffectGold);
                }
            );
        }

        StartCoroutine(IELoadScene());
    }

    IEnumerator IELoadScene()
    {
        yield return Yielders.Get(2.5f);
        GUIManager.Instance.LoadPlayScene();
    }

    IEnumerator IENextLevelAppear()
    {
        yield return Yielders.Get(2f);
        btn_NextLevel.gameObject.SetActive(true);
    }
}
