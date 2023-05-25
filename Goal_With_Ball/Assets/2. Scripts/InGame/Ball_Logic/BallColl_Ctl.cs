using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 1. 벽 or Goal 충돌 관리
/// 2. 충돌시 event 관리
/// </summary>
public class BallColl_Ctl : MonoBehaviour
{
    [SerializeField] private InGameManager inGameManager;

    [SerializeField] private bool isCameraZoom = false;
    
    public event Action BallColl;
    public event Action BallOut;
    public event Action BallCollToGoal;
    public event Action<bool> CameraZoom;
    
    private void Start()
    {
        inGameManager.gameEnd += PlayerBallDie;
    }

    private void PlayerBallDie()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
            BallColl?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            isCameraZoom = false;
            BallCollToGoal?.Invoke();
            Debug.Log("Goal");
            
        }
        else if (other.gameObject.CompareTag("OutWall"))
        {
            isCameraZoom = false;
            BallOut?.Invoke();
            
        }
        else if (other.gameObject.CompareTag("ZoomZone") && inGameManager.Life == 0)
        {
            Debug.Log("Zoom Start");
            isCameraZoom = true;
            CameraZoom?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ZoomZone") && isCameraZoom)
        {
            Debug.Log("Zoom Out");

            isCameraZoom = false;
            CameraZoom?.Invoke(false);
        }
    }

    #region Event 구독 해제
    
    private void OnDisable()
    {
        inGameManager.gameOver -= PlayerBallDie;
    }
    
    #endregion
}
