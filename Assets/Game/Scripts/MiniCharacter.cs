using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCharacter : Singleton<MiniCharacter>
{
    public GameObject g_Char;

    public void SpawnMiniCharacter(int _index)
    {
        int _id = ProfileManager.GetSelectedCharacter();

        Vector3 a = new Vector3(0f, -1.28f, 12.32f);

        if (g_Char != null)
        {
            Destroy(g_Char);
            g_Char = null;
        }

        g_Char = PrefabManager.Instance.SpawnMiniCharacter(_index);
        g_Char.transform.SetParent(gameObject.transform);
        g_Char.transform.eulerAngles = new Vector3(0f, -180f, 0f);
        g_Char.transform.localPosition = a;
        // g_Char.GetComponent<MiniCharacter>().m_Anim.SetTrigger(_anim);
    }
}
