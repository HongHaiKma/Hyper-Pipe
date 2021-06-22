using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BonusRewardCell : MonoBehaviour
{
    public Button btn_Claim;
    public GameObject g_Gold;
    public GameObject g_Char;
    public GameObject g_Chest;
    public TextMeshProUGUI txt_Gold;
    public Image img_Char;
    public BigNumber m_Gold = new BigNumber(0);
    public bool m_IsChar;

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_Claim, Claim);
    }

    private void OnEnable()
    {
        btn_Claim.interactable = true;
        Reset();
    }

    public void Reset()
    {
        g_Chest.SetActive(true);
        g_Char.SetActive(false);
        g_Gold.SetActive(false);
        btn_Claim.interactable = true;
    }

    public void Claim()
    {
        if (ProfileManager.GetKeys() > 0)
        {
            if (m_IsChar)
            {
                g_Char.SetActive(true);
                g_Gold.SetActive(false);
                ProfileManager.UnlockNewCharacter(PopupBonusReward.m_Char);
                ProfileManager.SetSelectedCharacter(PopupBonusReward.m_Char);
            }
            else
            {
                g_Char.SetActive(false);
                g_Gold.SetActive(true);
                ProfileManager.AddGold(m_Gold);
                EventManager.CallEvent(GameEvent.UPDATE_GOLD);
                SpawnGoldEffect();
            }

            g_Chest.SetActive(false);
            ProfileManager.AddKeys(-1);
            btn_Claim.interactable = false;
            PopupCaller.GetBonusRewardPopup().UpdateKeys();
        }
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
            g_EffectGold.transform.DOKill();
            g_EffectGold.transform.DOMove(PlaySceneManager.Instance.txt_TotalGold.gameObject.transform.position, 0.7f).SetDelay(0.1f + i * 0.1f).OnComplete(
                () =>
                {
                    PrefabManager.Instance.DespawnPool(g_EffectGold);
                }
            );
        }
    }
}
