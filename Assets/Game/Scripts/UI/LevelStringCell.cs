using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelStringCell : MonoBehaviour
{
    public Image img_BG;
    public int m_Level;
    public TextMeshProUGUI txt_Level;
    public RectTransform rect_Owner;
    public bool m_ProcessReward;

    public void SetupCell(int _level)
    {
        m_Level = _level;
        int level = m_Level + 1;
        if (level == ProfileManager.GetLevel())
        {
            rect_Owner.localScale = new Vector3(1.5f, 1.5f, 1f);
        }
        else
        {
            rect_Owner.localScale = new Vector3(1f, 1f, 1f);
        }

        if (!m_ProcessReward)
        {
            txt_Level.text = (m_Level + 1).ToString();
            if (level == ProfileManager.GetLevel())
            {
                img_BG.sprite = SpriteManager.Instance.m_LevelStringBG[1];
            }
            else
            {
                img_BG.sprite = SpriteManager.Instance.m_LevelStringBG[0];
            }
        }
    }
}
