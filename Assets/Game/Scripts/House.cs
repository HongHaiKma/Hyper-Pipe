using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject[] g_Waters;
    public bool m_Start = false;

    public float m_Time;
    public float m_TimeMax;

    public bool m_OutOfFire = false;

    public void OnEnable()
    {
        m_Time = 0f;
        m_TimeMax = 0.5f;
    }

    private void Update()
    {
        if (m_Start)
        {
            if (InGameObjectsManager.Instance.m_Char.m_RotatePipe)
            {
                if (m_Time >= m_TimeMax)
                {
                    Offffff();
                }
                else
                {
                    m_Time += Time.deltaTime;
                }
            }
        }
    }

    public void Offffff()
    {
        if (!CheckNotHaveWater())
        {
            InGameObjectsManager.Instance.m_Char.m_LastAction = false;
            PlaySceneManager.Instance.g_JoystickTrackPad.SetActive(false);
            PlaySceneManager.Instance.g_Hand.SetActive(false);
            Vector3 pos = InGameObjectsManager.Instance.m_Char.tf_Owner.position;
            PrefabManager.Instance.SpawnFirework(pos);
            CameraController.Instance.DoFinalAction();
            m_Start = false;
            return;
        }

        for (int i = 0; i < g_Waters.Length; i++)
        {
            if (g_Waters[i].activeInHierarchy)
            {
                g_Waters[i].SetActive(false);
                GameManager.Instance.Vibrate(0);
                if (!CheckNotHaveWater())
                {
                    InGameObjectsManager.Instance.m_Char.m_LastAction = false;
                    PlaySceneManager.Instance.g_JoystickTrackPad.SetActive(false);
                    PlaySceneManager.Instance.g_Hand.SetActive(false);
                    Vector3 pos = InGameObjectsManager.Instance.m_Char.tf_Owner.position;
                    PrefabManager.Instance.SpawnFirework(pos);
                    CameraController.Instance.DoFinalAction();
                    m_Start = false;
                    // SoundManager.Instance.OnSoundWin();
                    // if (ProfileManager.GetKeys() < 3)
                    // {
                    //     PopupCaller.OpenWinPopup();
                    // }
                    // else
                    // {
                    //     PopupCaller.OpenBonusRewardPopup();
                    // }
                    // m_Start = false;
                    // this.enabled = false;
                }
                break;
            }
        }

        m_Time = 0f;
    }

    public void HandleWin()
    {
        SoundManager.Instance.OnSoundWin();
        StartCoroutine(IEHandleWin());
    }

    IEnumerator IEHandleWin()
    {
        yield return Yielders.Get(3f);
        SoundManager.Instance.PlaySoundWinLong(true);
        // yield return Yielders.Get(3f);
        int level = ProfileManager.GetLevel();

        // if (level >= 3 && (level % 2 == 1))
        // {
        //     AdsManager.Instance.WatchInterstitial();
        // }

        if (ProfileManager.GetKeys() < 3)
        {
            PopupCaller.OpenWinPopup();
        }
        else
        {
            PopupCaller.OpenBonusRewardPopup();
        }
        this.enabled = false;
    }

    public bool CheckNotHaveWater()
    {
        bool aaa = false;

        for (int i = 0; i < g_Waters.Length; i++)
        {
            if (g_Waters[i].activeInHierarchy)
            {
                aaa = true;
                break;
            }
        }

        return aaa;
    }
}
