using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBonusReward : UICanvas
{
    public List<BonusRewardCell> m_BonusCells = new List<BonusRewardCell>();
    public static int m_Char;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetupRandom();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // ProfileManager.UnlockNewCharacter(CharacterType.WORKER);
            SetupRandom();
        }
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
        }

        Dictionary<int, BonusRewardConfig> bonusLength = GameData.Instance.GetBonusRewardConfig();

        List<BonusRewardConfig> rewardConfigs = new List<BonusRewardConfig>();

        for (int i = 0; i < rewardConfigs.Count; i++)
        {
            rewardConfigs.Add(bonusLength[i + 1]);
        }

        Helper.DebugLog(cells.Count);

        for (int i = 0; i < cells.Count; i++)
        {
            Helper.DebugLog("iiiii :" + i);
            int random = Random.Range(0, rewardConfigs.Count);
            BonusRewardConfig reward = rewardConfigs[random];
            cells[i].m_Gold = reward.m_Gold;
            cells[i].txt_Gold.text = reward.m_Gold.ToString();

            if (reward.m_Slot == 9 && count > 0)
            {
                m_Char = charId;
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
}
