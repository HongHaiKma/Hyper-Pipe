using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public Sprite[] m_CharCards;
    public Sprite[] m_UICardBG;
    public Sprite[] m_LevelStringBG;
    public Sprite[] m_Settings;
}

public enum MiscSpriteKeys
{
    UI_CARD_BG_LOCK = 0,
    UI_CARD_BG_UNLOCK = 1,
}