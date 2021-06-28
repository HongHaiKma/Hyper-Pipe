using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem particle;

    private float time;
    private float timeMax = 2f;

    private void Update()
    {
        if (time < timeMax)
        {
            time += Time.deltaTime;

        }
        else
        {
            time = 0f;
            particle.Play();
        }
    }
}
