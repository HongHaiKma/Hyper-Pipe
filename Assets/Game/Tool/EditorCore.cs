using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCore : MonoBehaviour
{
    public List<GameObject> m_PipeCollect;
    public List<GameObject> m_Pipes;

    public void ChangePipe()
    {
        List<Vector3> pos = new List<Vector3>();
        for (int i = 0; i < m_Pipes.Count; i++)
        {
            pos.Add(m_Pipes[i].transform.position);
        }
        for (int i = 0; i < m_Pipes.Count; i++)
        {
            DestroyImmediate(m_Pipes[i]);
        }
        m_Pipes.Clear();
        for (int i = 0; i < pos.Count; i++)
        {
            GameObject pipeCollect = Instantiate(m_PipeCollect[Random.Range(0, m_PipeCollect.Count)]);
            pipeCollect.transform.position = pos[i];
            m_Pipes.Add(pipeCollect);
        }
    }
}
