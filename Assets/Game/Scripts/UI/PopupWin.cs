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

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        GUIManager.Instance.AddClickEvent(btn_NextLevel, OnNextLevel);


        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    public override void OnEnable()
    {
        txt_GoldWin.text = GameManager.Instance.m_GoldWin.ToString();
        base.OnEnable();
    }

    public void OnNextLevel()
    {
        Time.timeScale = 1;
        GUIManager.Instance.LoadPlayScene();
    }

    public void SpawnGoldEffect()
    {
        InGameObjectsManager.Instance.DespawnGoldEffectPool();

        for (int i = 0; i < 15; i++)
        {
            GameObject g_EffectGold = PrefabManager.Instance.SpawnGoldEffect(ConfigKeys.m_GoldEffect1, transform.position);
            g_EffectGold.transform.SetParent(this.transform);
            g_EffectGold.transform.localScale = new Vector3(1, 1, 1);
            g_EffectGold.transform.position = transform.position;

            InGameObjectsManager.Instance.g_GoldEffects.Add(g_EffectGold);

            g_EffectGold.transform.DOMove(PlaySceneManager.Instance.txt_TotalGold.gameObject.transform.position, 0.7f).SetDelay(0.1f + i * 0.1f).OnComplete(
                () =>
                {
                    PrefabManager.Instance.DespawnPool(g_EffectGold);
                }
            );
        }
    }
}
