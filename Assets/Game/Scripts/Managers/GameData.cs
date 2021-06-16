using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[DefaultExecutionOrder(-95)]
public class GameData : Singleton<GameData>
{
    public List<TextAsset> m_DataText = new List<TextAsset>();

    private Dictionary<int, CharacterDataConfig> m_CharacterDataConfigs = new Dictionary<int, CharacterDataConfig>();
    private Dictionary<int, LevelConfig> m_LevelConfigs = new Dictionary<int, LevelConfig>();
    private Dictionary<int, BonusRewardConfig> m_BonusRewardConfigs = new Dictionary<int, BonusRewardConfig>();

    private void Awake()
    {
        LoadCharacterConfig();
        LoadLevelConfig();
        LoadBonusRewardConfig();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // for (int i = 0; i < GetBonusRewardConfig().Count; i++)
            // {
            //     Helper.DebugLog(GetBonusRewardConfig()[i].m_Slot);
            //     Helper.DebugLog(GetBonusRewardConfig()[i].m_Gold.ToString());
            // }
            // for (int i = 0; i < GetEpicCharacterDataConfig().Count; i++)
            // {
            //     Helper.DebugLog(GetEpicCharacterDataConfig()[i].m_Name);
            // }
        }
    }

    public void LoadCharacterConfig()
    {
        m_CharacterDataConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.DATA_CHAR);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["ID"]);

            string name = "";
            if (iNode["Name"].ToString().Length > 0)
            {
                name = iNode["Name"];
            }

            string colName = "";

            BigNumber price = 0;
            colName = "Price";
            if (iNode[colName].ToString().Length > 0)
            {
                price = new BigNumber(iNode[colName]) + 0;
            }

            int adsCheck = 0;
            colName = "AdsCheck";
            if (iNode[colName].ToString().Length > 0)
            {
                adsCheck = int.Parse(iNode[colName]);
            }

            int adsNumber = 0;
            colName = "AdsNumber";
            if (iNode[colName].ToString().Length > 0)
            {
                adsNumber = int.Parse(iNode[colName]);
            }

            int rarity = 0;
            colName = "Rarity";
            if (iNode[colName].ToString().Length > 0)
            {
                rarity = int.Parse(iNode[colName]);
            }

            CharacterDataConfig character = new CharacterDataConfig();
            character.Init(id, name, price, adsCheck, adsNumber, rarity);
            m_CharacterDataConfigs.Add(id, character);
        }
    }

    public void LoadLevelConfig()
    {
        m_LevelConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.LEVEL_CONfIG);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["ID"]);

            string colName = "";

            BigNumber maxLevel = 0;
            colName = "MaxLevel";
            if (iNode[colName].ToString().Length > 0)
            {
                maxLevel = new BigNumber(iNode[colName]) + 0;
            }

            BigNumber minGold = 0;
            colName = "MinGold";
            if (iNode[colName].ToString().Length > 0)
            {
                minGold = new BigNumber(iNode[colName]) + 0;
            }

            LevelConfig levelConfig = new LevelConfig();
            levelConfig.Init(id, maxLevel, minGold);
            m_LevelConfigs.Add(id, levelConfig);
        }
    }

    public void LoadBonusRewardConfig()
    {
        m_BonusRewardConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.BONUS_REWARD);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["Slot"]);

            string colName = "";

            BigNumber gold = 0;
            colName = "Gold";
            if (iNode[colName].ToString().Length > 0)
            {
                gold = new BigNumber(iNode[colName]) + 0;
            }

            BonusRewardConfig bonusRewardConfigs = new BonusRewardConfig();
            bonusRewardConfigs.Init(id, gold);
            m_BonusRewardConfigs.Add(id, bonusRewardConfigs);
            Helper.DebugLog("Bonus data row 1");
        }
    }

    public TextAsset GetDataAssets(GameDataType _id)
    {
        return m_DataText[(int)_id];
    }

    public CharacterDataConfig GetCharacterDataConfig(int charID)
    {
        return m_CharacterDataConfigs[charID];
    }
    public CharacterDataConfig GetCharacterDataConfig(CharacterType characterType)
    {
        return m_CharacterDataConfigs[(int)characterType];
    }
    public Dictionary<int, CharacterDataConfig> GetCharacterDataConfig()
    {
        return m_CharacterDataConfigs;
    }

    public List<CharacterDataConfig> GetLegendCharacterDataConfig()
    {
        List<CharacterDataConfig> configs = new List<CharacterDataConfig>();
        int count = m_CharacterDataConfigs.Count;
        for (int i = 1; i <= count; i++)
        {
            if (m_CharacterDataConfigs[i].GetRatity() == (int)OutfitRarity.LEGEND)
            {
                configs.Add(m_CharacterDataConfigs[i]);
            }
        }
        return configs;
    }

    public CharacterDataConfig GetLegendCharacterDataConfig(int _id)
    {
        List<CharacterDataConfig> configs = new List<CharacterDataConfig>();
        int count = m_CharacterDataConfigs.Count;
        for (int i = 1; i <= count; i++)
        {
            if (m_CharacterDataConfigs[i].GetRatity() == (int)OutfitRarity.LEGEND)
            {
                configs.Add(m_CharacterDataConfigs[i]);
            }
        }
        return configs[_id];
    }

    public List<CharacterDataConfig> GetEpicCharacterDataConfig()
    {
        List<CharacterDataConfig> configs = new List<CharacterDataConfig>();
        int count = m_CharacterDataConfigs.Count;
        for (int i = 1; i <= count; i++)
        {
            if (m_CharacterDataConfigs[i].GetRatity() == (int)OutfitRarity.EPIC)
            {
                configs.Add(m_CharacterDataConfigs[i]);
            }
        }
        return configs;
    }

    public Dictionary<int, LevelConfig> GetLevelConfig()
    {
        return m_LevelConfigs;
    }

    public Dictionary<int, BonusRewardConfig> GetBonusRewardConfig()
    {
        return m_BonusRewardConfigs;
    }

    public enum GameDataType
    {
        DATA_CHAR = 0,
        LEVEL_CONfIG = 1,
        BONUS_REWARD = 2,
    }
}