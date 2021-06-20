using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Score : InGameObject
{
    public static int m_Score;
    public int m_ScoreLine;
    public TextMeshProUGUI txt_Score;
    public Image img_BG;

    public override void StartListenToEvents()
    {
        // EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager1<int>.AddListener(GameEvent.DESTROY_SCORE_LINE, Event_SCORE_LINE_PICK);
    }

    public override void StopListenToEvents()
    {
        // EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager1<int>.RemoveListener(GameEvent.DESTROY_SCORE_LINE, Event_SCORE_LINE_PICK);
    }

    // public void Event_SCORE_LINE_PICK(bool _logic)
    // {
    //     if (_logic)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public void Event_SCORE_LINE_PICK(int _result)
    {
        Character charrr = InGameObjectsManager.Instance.m_Char;
        if (_result < 7)
        {
            if (m_ScoreLine == _result)
            {
                if (m_ScoreLine < 3)
                {
                    InGameObjectsManager.Instance.m_House = PrefabManager.Instance.SpawnHouse(0, tf_Owner.position).GetComponent<House>();
                    Helper.DebugLog("m_ScoreLine = " + m_ScoreLine);
                }
                else if (m_ScoreLine <= 6)
                {
                    InGameObjectsManager.Instance.m_House = PrefabManager.Instance.SpawnHouse(1, tf_Owner.position).GetComponent<House>();
                    Helper.DebugLog("m_ScoreLine = " + m_ScoreLine);
                }
                Vector3 planeHouse = InGameObjectsManager.Instance.m_House.transform.position;
                PrefabManager.Instance.SpawnPlaneHouse(planeHouse);
                charrr.tf_Owner.DOMove(tf_Owner.position, 2f).OnComplete
                (
                    () =>
                    {
                        charrr.ChangeState(IdleState.Instance);
                        charrr.m_LastAction = true;
                        EventManager1<bool>.CallEvent(GameEvent.WATER, true);
                        InGameObjectsManager.Instance.m_House.m_Start = true;
                        CameraController.Instance.DoLastAction();
                    }
                );

                InGameObjectsManager.Instance.DespawnRedundantScoreLines(m_ScoreLine);
            }
        }
        else
        {
            if (m_ScoreLine == 7)
            {
                Helper.DebugLog("m_ScoreLine = " + m_ScoreLine);
                InGameObjectsManager.Instance.m_House = PrefabManager.Instance.SpawnHouse(2, tf_Owner.position).GetComponent<House>();
                Vector3 planeHouse = InGameObjectsManager.Instance.m_House.transform.position;
                PrefabManager.Instance.SpawnPlaneHouse(planeHouse);
                charrr.tf_Owner.DOMove(tf_Owner.position, 2f).OnComplete
                (
                    () =>
                    {
                        charrr.ChangeState(IdleState.Instance);
                        charrr.m_LastAction = true;
                        EventManager1<bool>.CallEvent(GameEvent.WATER, true);
                        InGameObjectsManager.Instance.m_House.m_Start = true;
                        CameraController.Instance.DoLastAction();
                    }
                );

                InGameObjectsManager.Instance.DespawnRedundantScoreLines(m_ScoreLine);
            }
        }
    }


    public void SetScore(Color _color)
    {
        m_Score++;
        // m_ScoreLine = _scoreLine;
        m_ScoreLine = m_Score;
        txt_Score.text = "x" + m_Score.ToString();
        img_BG.color = new Color(_color.r, _color.g, _color.b);
    }

    public void Blink()
    {
        float r = img_BG.color.r;
        float g = img_BG.color.g;
        float b = img_BG.color.b;
        img_BG.DOColor(Color.white, 0.3f).OnComplete
        (
            () => img_BG.DOColor(new Color(r, g, b), 0.5f)
        );
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}