using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[DefaultExecutionOrder(-92)]
public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public GameObject g_Truck;
    public float m_MapLength;

    public GameObject g_Map;

    public House m_House;

    public int m_MapMinConfig;
    public int m_MapMaxConfig;

    public int m_MapPrefabMinConfig;
    public int m_MapPrefabMaxConfig;

    private int m_MapMin;
    private int m_MapMax;

    private int m_MapPrefabMin;
    private int m_MapPrefabMax;

    public List<LevelMapConfig> m_LevelConfigs;

    public GameObject g_Ending;

    public int m_ScoreLine;

    public List<GameObject> g_GoldEffects = new List<GameObject>();
    public List<GameObject> g_ScoreLines = new List<GameObject>();

    public bool m_Play10Lelevel;

    [Header("Level")]
    public int level;

    public override void OnEnable()
    {
        base.OnEnable();
        DespawnAllPools();
        m_ScoreLine = 1;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     // for (int i = 0; i < GetBonusRewardConfig().Count; i++)
        //     // {
        //     //     Helper.DebugLog(GetBonusRewardConfig()[i].m_Slot);
        //     //     Helper.DebugLog(GetBonusRewardConfig()[i].m_Gold.ToString());
        //     // }
        //     // DespawnGoldEffectPool();
        //     g_GoldEffects.Clear();
        // }
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
    }

    public void Event_END_GAME()
    {
        m_ScoreLine = 1;
    }

    public void DespawnAllPools()
    {
        DespawnGoldEffectPool();
        DespawnScoreLines();
    }

    public void DespawnGoldEffectPool()
    {
        int count = g_GoldEffects.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                // g_GoldEffects.Dokill
                // g_GoldEffects[i].transform.DOComplete();
                g_GoldEffects[i].transform.DOKill();
                PrefabManager.Instance.DespawnPool(g_GoldEffects[i]);
                // g_GoldEffects.Remove(g_GoldEffects[i]);
            }
        }
        g_GoldEffects.Clear();
    }

    public void DespawnScoreLines()
    {
        int count = g_ScoreLines.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                PrefabManager.Instance.DespawnPool(g_ScoreLines[i]);
            }
        }
        g_ScoreLines.Clear();
    }

    public void DespawnRedundantScoreLines(int _scoreLine)
    {
        int count = g_ScoreLines.Count;
        for (int i = _scoreLine; i < count; i++)
        {
            PrefabManager.Instance.DespawnPool(g_ScoreLines[i]);
        }
        g_ScoreLines.Clear();
    }

    public void LoadMap()
    {
        EventManager.CallEvent(GameEvent.LOAD_MAP);

        CameraController.Instance.g_Wind.SetActive(false);

        Score.m_Score = 0;
        m_MapLength = 0;

        int level = ProfileManager.GetLevel();
        this.level = level;

        if (m_Play10Lelevel)
        {
            if (level <= 10)
            {
                m_MapMin = m_LevelConfigs[level - 1].m_MapMin;
                m_MapMax = m_LevelConfigs[level - 1].m_MapMax;
                m_MapPrefabMin = m_LevelConfigs[level - 1].m_MapPrefabMin;
                m_MapPrefabMax = m_LevelConfigs[level - 1].m_MapPrefabMax;
            }
            else
            {
                m_MapMin = m_MapMinConfig;
                m_MapMax = m_MapMaxConfig;
                m_MapPrefabMin = m_MapPrefabMinConfig;
                m_MapPrefabMax = m_MapPrefabMaxConfig;
            }
        }
        else
        {
            m_MapMin = m_MapMinConfig;
            m_MapMax = m_MapMaxConfig;
            m_MapPrefabMin = m_MapPrefabMinConfig;
            m_MapPrefabMax = m_MapPrefabMaxConfig;
        }

        int mapLengthRandom = Random.Range(m_MapMin, m_MapMax + 1);

        List<GameObject> keysInGame = new List<GameObject>();

        LevelEasyConfig lv = new LevelEasyConfig();

        if (level <= 10)
        {
            lv = GameData.Instance.GetLevelEasyConfig(level);
        }

        List<int> listMap = new List<int>();
        listMap.Add(lv.m_PathCell1);
        listMap.Add(lv.m_PathCell2);
        listMap.Add(lv.m_PathCell3);
        listMap.Add(lv.m_PathCell4);

        if (level <= 10)
        {
            Helper.DebugLog("Level <= 10");
            mapLengthRandom = 4;
            for (int i = 0; i < mapLengthRandom; i++)
            {
                int mapCellRandom = listMap[i];
                GameObject go = PrefabManager.Instance.SpawnPathCell(mapCellRandom, m_MapLength);
                go.transform.parent = PlaySceneManager.Instance.g_Map.transform;
                PathCell cell = go.GetComponent<PathCell>();
                if (cell.g_KeyInGame != null)
                {
                    keysInGame.Add(cell.g_KeyInGame);
                    Helper.DebugLog("Add key in Game");
                }
                m_MapLength += cell.CalculateTotalLength();
            }
        }
        else
        {
            for (int i = 0; i < mapLengthRandom; i++)
            {
                int mapCellRandom = Random.Range(m_MapPrefabMin - 1, m_MapPrefabMax);
                GameObject go = PrefabManager.Instance.SpawnPathCell(mapCellRandom, m_MapLength);
                go.transform.parent = PlaySceneManager.Instance.g_Map.transform;
                PathCell cell = go.GetComponent<PathCell>();
                if (cell.g_KeyInGame != null)
                {
                    keysInGame.Add(cell.g_KeyInGame);
                    Helper.DebugLog("Add key in Game");
                }
                m_MapLength += cell.CalculateTotalLength();
            }
        }

        for (int i = 0; i < keysInGame.Count; i++)
        {
            keysInGame[i].SetActive(false);
        }

        if (level == 1)
        {
            keysInGame[Random.Range(0, keysInGame.Count)].SetActive(true);
        }
        else if ((level % GameManager.Instance.m_KeyInGameStep) == 1)
        {
            Helper.DebugLog("Enable key in game");
            keysInGame[Random.Range(0, keysInGame.Count)].SetActive(true);
        }

        GameObject ending = PrefabManager.Instance.SpawnEnding(m_MapLength - 21f);
        g_Ending = ending;
        ending.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
        m_MapLength += ending.GetComponent<BoxCollider>().size.y * 10f;

        GameObject score = PrefabManager.Instance.SpawnScoreLine(ConfigKeys.m_ScoreLine, new Vector3(0f, 0f, ending.transform.position.z + 4f + 5f));
        score.GetComponent<Score>().SetScore(GameManager.Instance.m_ScoreLineColor[0]);

        for (int i = 0; i < 7; i++)
        {
            GameObject go = PrefabManager.Instance.SpawnScoreLine(ConfigKeys.m_ScoreLine, new Vector3(0f, 0f, InGameObjectsManager.Instance.g_Ending.transform.position.z + Score.m_Score * 2 * 7 + 9f));
            go.GetComponent<Score>().SetScore(GameManager.Instance.m_ScoreLineColor[Score.m_Score % 7]);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject go = PrefabManager.Instance.SpawnPathCell0(m_MapLength - 5f);
            go.transform.parent = PlaySceneManager.Instance.g_Map.transform;
            PathCell cell = go.GetComponent<PathCell>();
            m_MapLength += cell.CalculateTotalLength();
        }

        GameObject truck = PrefabManager.Instance.SpawnTruck(0);
        g_Truck = truck;

        CameraController.Instance.m_CMFreeLook.Follow = truck.transform;

    }

    public void StartMoveTruck()
    {
        PlaySceneManager.Instance.btn_MoveTruck.gameObject.SetActive(false);
        PlaySceneManager.Instance.btn_Outfit.gameObject.SetActive(false);
        g_Truck.transform.DOMove(new Vector3(0f, 0f, -54.2f), 2.5f).OnComplete
        (
            () =>
            {
                // EventManager1<bool>.CallEvent(GameEvent.GAME_START, true);

                int index = ProfileManager.GetSelectedChar();
                GameObject charrr = PrefabManager.Instance.SpawnChar(index - 1);
                m_Char = charrr.GetComponent<Character>();
                CameraController.Instance.m_CMFreeLook.Follow = null;

                g_Truck.transform.DOMove(new Vector3(-25f, 0f, -54.2f), 0.5f).OnComplete(
                    () =>
                    {
                        CameraController.Instance.Do1stAction();
                        // PlaySceneManager.Instance.btn_Outfit.gameObject.SetActive(true);
                        PlaySceneManager.Instance.g_LevelString.gameObject.SetActive(true);
                        PlaySceneManager.Instance.btn_StartLonger.gameObject.SetActive(true);
                        PlaySceneManager.Instance.m_TouchTrackPad.gameObject.SetActive(true);
                        PlaySceneManager.Instance.btn_StartLonger.gameObject.SetActive(true);
                    }
                );
            }
        );
    }

    public void LoadChar(int _characterType)
    {
        if (m_Char != null)
        {
            Helper.DebugLog("3333333333333333333333333333");
            Destroy(m_Char.gameObject);
            m_Char = null;
            int index = ProfileManager.GetSelectedChar();
            GameObject charrr = PrefabManager.Instance.SpawnChar(index - 1);
            m_Char = charrr.GetComponent<Character>();
            CameraController.Instance.m_CMFreeLook.Follow = charrr.transform;
        }
        // else
        // {
        //     ProfileManager.SetSelectedCharacter(_characterType);
        // }
    }
}

[System.Serializable]
public struct LevelMapConfig
{
    public int m_MapMin;
    public int m_MapMax;

    public int m_MapPrefabMin;
    public int m_MapPrefabMax;
}