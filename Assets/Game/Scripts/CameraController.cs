using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraController : Singleton<CameraController>
{
    public Transform tf_Owner;
    public CinemachineFreeLook m_CMFreeLook;
    public CinemachineCameraOffset m_CMOffset;
    public GameObject g_Wind;

    [Header("Lerp cam follow pos")]
    float timeElapsed;
    float lerpDuration = 3;

    float startValue = 2.23f;
    float endValue = 0f;
    float valueToLerp;

    [Header("Last Action")]
    float timeElapsedLastActionX;
    float lerpDurationLastActionX = 3;

    float startValueLastActionX = 0f;
    float endValueLastActionX = 3.1f;

    float startValueLastActionY = 5.8f;
    float endValueLastActionY = 1.4f;

    float startValueLastActionZ = -26.4f;
    float endValueLastActionZ = -8.8f;

    float valueToLerpLastActionX;
    float valueToLerpLastActionY;
    float valueToLerpLastActionZ;

    public override void OnEnable()
    {
        timeElapsed = 4f;
        timeElapsedLastActionX = 4f;
    }

    private void Update()
    {
        // if (timeElapsed < lerpDuration)
        // {
        //     valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
        //     m_CMOffset.m_Offset.y = valueToLerp;
        //     timeElapsed += Time.deltaTime;
        // }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DoLastAction();
            // DoActionByPath();
        }
    }

    public void DoActionByPath()
    {
        tf_Owner.DORotate(new Vector3(7f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed = 0f;
        StartCoroutine(ChangeCMOffset(true));
    }

    public void DoLastAction()
    {
        tf_Owner.DORotate(new Vector3(0f, -90f, 0f), 1.5f, RotateMode.Fast);
        timeElapsedLastActionX = 0f;
        StartCoroutine(ChangeCMOffsetLastAction());
    }

    public void UndoActionByPath()
    {
        tf_Owner.DORotate(new Vector3(19f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed = 0f;
        StartCoroutine(ChangeCMOffset(false));
    }

    IEnumerator ChangeCMOffsetLastAction()
    {
        while (timeElapsedLastActionX < lerpDurationLastActionX)
        {
            valueToLerpLastActionX = Mathf.Lerp(startValueLastActionX, endValueLastActionX, timeElapsedLastActionX / lerpDurationLastActionX);
            valueToLerpLastActionY = Mathf.Lerp(startValueLastActionY, endValueLastActionY, timeElapsedLastActionX / lerpDurationLastActionX);
            valueToLerpLastActionZ = Mathf.Lerp(startValueLastActionZ, endValueLastActionZ, timeElapsedLastActionX / lerpDurationLastActionX);

            m_CMOffset.m_Offset.x = valueToLerpLastActionX;
            m_CMOffset.m_Offset.y = valueToLerpLastActionY;
            m_CMOffset.m_Offset.z = valueToLerpLastActionZ;

            timeElapsedLastActionX += Time.deltaTime;
        }
        yield return new WaitUntil(() => timeElapsedLastActionX >= lerpDurationLastActionX);
    }

    IEnumerator ChangeCMOffset(bool _hanging)
    {
        while (timeElapsed < lerpDuration)
        {
            if (_hanging)
            {
                valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            }
            else
            {
                valueToLerp = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
            }
            m_CMOffset.m_Offset.y = valueToLerp;
            timeElapsed += Time.deltaTime;
        }
        yield return new WaitUntil(() => timeElapsed >= lerpDuration);
    }

    public void TestCinematic()
    {
        tf_Owner.DORotate(new Vector3(19f, 178.81f, 0f), 1.5f);
    }
}
