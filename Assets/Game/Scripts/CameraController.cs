using System.Collections;
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
    float endValueLastActionX = 3f;

    float startValueLastActionY = 5.8f;
    float endValueLastActionY = 1.4f;

    float startValueLastActionZ = -26.4f;
    float endValueLastActionZ = -8.8f;

    float valueToLerpLastActionX;
    float valueToLerpLastActionY;
    float valueToLerpLastActionZ;

    [Header("Last Action")]
    float timeElapsed1stActionX;
    float lerpDuration1stActionX = 3;

    float startValue1stActionX = -4.16f;
    float endValue1stActionX = 0f;

    float startValue1stActionY = -0.04f;
    float endValue1stActionY = 2.8f;

    float startValue1stActionZ = -28.47f;
    float endValue1stActionZ = -3.47f;

    float valueToLerp1stActionX;
    float valueToLerp1stActionY;
    float valueToLerp1stActionZ;

    public override void OnEnable()
    {
        timeElapsed = 4f;
        timeElapsedLastActionX = 4f;
        timeElapsed1stActionX = 4f;
    }

    // private void Update()
    // {
    //     // if (timeElapsed < lerpDuration)
    //     // {
    //     //     valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
    //     //     m_CMOffset.m_Offset.y = valueToLerp;
    //     //     timeElapsed += Time.deltaTime;
    //     // }

    //     // if (Input.GetKeyDown(KeyCode.M))
    //     // {
    //     //     DoLastAction();
    //     //     // DoActionByPath();
    //     // }
    // }

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

    public void DoFinalAction()
    {
        Character charrr = InGameObjectsManager.Instance.m_Char;
        charrr.tf_PipeHolder.gameObject.SetActive(false);
        charrr.anim_Owner.SetTrigger("Win");
        tf_Owner.DORotate(new Vector3(0f, -270f, 0f), 1.5f, RotateMode.Fast).OnComplete(
            () =>
            {
                InGameObjectsManager.Instance.m_House.HandleWin();
            }
        );
        StartCoroutine(ChangeCMOffsetFinalAction());
        EventManager1<bool>.CallEvent(GameEvent.WATER, false);
    }

    public void UndoActionByPath()
    {
        tf_Owner.DORotate(new Vector3(19f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed = 0f;
        StartCoroutine(ChangeCMOffset(false));
    }

    public void Do1stAction()
    {
        tf_Owner.DORotate(new Vector3(40f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed1stActionX = 0f;
        StartCoroutine(ChangeCMOffset1stAction());
    }

    IEnumerator ChangeCMOffset1stAction()
    {
        // CameraController.Instance.m_CMFreeLook.Follow = InGameObjectsManager.Instance.m_Char.transform;
        while (timeElapsed1stActionX < lerpDuration1stActionX)
        {
            valueToLerp1stActionX = Mathf.Lerp(startValue1stActionX, endValue1stActionX, timeElapsed1stActionX / lerpDuration1stActionX);
            valueToLerp1stActionY = Mathf.Lerp(startValue1stActionY, endValue1stActionY, timeElapsed1stActionX / lerpDuration1stActionX);
            valueToLerp1stActionZ = Mathf.Lerp(startValue1stActionZ, endValue1stActionZ, timeElapsed1stActionX / lerpDuration1stActionX);

            m_CMOffset.m_Offset.x = valueToLerp1stActionX;
            m_CMOffset.m_Offset.y = valueToLerp1stActionY;
            m_CMOffset.m_Offset.z = valueToLerp1stActionZ;

            timeElapsed1stActionX += Time.deltaTime;
        }
        yield return new WaitUntil(() => timeElapsed1stActionX >= lerpDuration1stActionX);
        // CameraController.Instance.m_CMFreeLook.LookAt = InGameObjectsManager.Instance.m_Char.transform;
        CameraController.Instance.m_CMFreeLook.Follow = InGameObjectsManager.Instance.m_Char.transform;
    }

    IEnumerator ChangeCMOffsetLastAction()
    {
        CameraController.Instance.m_CMFreeLook.m_Lens.FieldOfView = 40f;
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
        g_Wind.SetActive(false);
    }

    IEnumerator ChangeCMOffsetFinalAction()
    {
        float valueToLerpFinalActionX = 0f;
        float valueToLerpFinalActionY = 0f;
        float valueToLerpFinalActionZ = 0f;

        float timeElapsedFinalActionX = 0f;
        float lerpDurationFinalActionX = 3f;

        float endValueFinalActionX = -3.25f;
        float endValueFinalActionY = -0.44f;
        float endValueFinalActionZ = -12.72f;

        float startValueFinalActionX = m_CMOffset.m_Offset.x;
        float startValueFinalActionY = m_CMOffset.m_Offset.y;
        float startValueFinalActionZ = m_CMOffset.m_Offset.z;

        CameraController.Instance.m_CMFreeLook.m_Lens.FieldOfView = 40f;
        while (timeElapsedFinalActionX < lerpDurationFinalActionX)
        {
            valueToLerpFinalActionX = Mathf.Lerp(startValueFinalActionX, endValueFinalActionX, timeElapsedFinalActionX / lerpDurationFinalActionX);
            valueToLerpFinalActionY = Mathf.Lerp(startValueFinalActionY, endValueFinalActionY, timeElapsedFinalActionX / lerpDurationFinalActionX);
            valueToLerpFinalActionZ = Mathf.Lerp(startValueFinalActionZ, endValueFinalActionZ, timeElapsedFinalActionX / lerpDurationFinalActionX);

            m_CMOffset.m_Offset.x = valueToLerpFinalActionX;
            m_CMOffset.m_Offset.y = valueToLerpFinalActionY;
            m_CMOffset.m_Offset.z = valueToLerpFinalActionZ;

            timeElapsedFinalActionX += Time.deltaTime;
        }
        yield return new WaitUntil(() => timeElapsedFinalActionX >= lerpDurationFinalActionX);
        g_Wind.SetActive(false);
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
