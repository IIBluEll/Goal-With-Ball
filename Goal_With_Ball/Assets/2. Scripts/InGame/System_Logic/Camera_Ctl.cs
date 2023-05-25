using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 공이 골인 지점에 근접하면 가까워지며 슬로우화면으로 전환
/// 2. 카메라 zoom zone을 벗어나면 원래대로 돌아감
/// 3. Zoom In 중에는 UI 렌더링 하지 않음
/// 4. Zoom Out 될때는 UI를 다시 렌더링함
/// </summary>
public class Camera_Ctl : MonoBehaviour
{
    private Camera thisCam;
    
    [SerializeField] private BallColl_Ctl ballColl;
    [SerializeField] private Transform target;
    
    [SerializeField] private float zoomSize;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float followSpeed = 5f;

    private float originSize;
    private bool isZoom = false;
    
    private Vector3 originPos;

    private void OnEnable()
    {
        thisCam = this.GetComponent<Camera>();
        
        ballColl.CameraZoom += ZoomCamera;
    }

    private void Start()
    {
        originSize = thisCam.orthographicSize;
        originPos = thisCam.transform.position;
    }

    private void ZoomCamera(bool zoomIn)
    {
        if (zoomIn)
        {
            StartCoroutine(ZoomIn());
            thisCam.cullingMask = ~(1 << LayerMask.NameToLayer("UI"));
        }
        else
        {
            StartCoroutine(ZoomOut());
            thisCam.cullingMask |= 1 << LayerMask.NameToLayer("UI");
        }
    }
    
    private IEnumerator ZoomIn()
    {
        isZoom = true;
        
        var time = 0f;
        var startPos = thisCam.transform.position;
        var startSize = thisCam.orthographicSize;

        while (time < duration)
        {
            thisCam.transform.position = Vector3.Lerp(startPos, target.position, time / duration);
            thisCam.orthographicSize = Mathf.Lerp(startSize, zoomSize, time / duration);

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        thisCam.transform.position = target.position;
        thisCam.orthographicSize = zoomSize;

        isZoom = false;
    }
    
    private IEnumerator ZoomOut()
    {
        isZoom = true;
        var time = 0f;
        var startPos = thisCam.transform.position;
        var startSize = thisCam.orthographicSize;

        while (time < .5f)
        {
            thisCam.transform.position = Vector3.Lerp(startPos, originPos, time / .5f);
            thisCam.orthographicSize = Mathf.Lerp(startSize, originSize, time / .5f);

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        thisCam.transform.position = originPos;
        thisCam.orthographicSize = originSize;
        
        isZoom = false;
    }

    #region Event 구독 해제

    private void OnDisable()
    {
        ballColl.CameraZoom -= ZoomCamera;
    }

    #endregion
}
