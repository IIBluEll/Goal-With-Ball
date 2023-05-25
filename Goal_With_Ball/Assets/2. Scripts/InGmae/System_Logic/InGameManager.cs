using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. LoadLevel를 통해 현재 스테이지 data를 받아옴
/// 2. 카메라 및 Ball를 현재 스테이지에 맞게 위치 이동 
/// 3. Ball 의 Life 갯수 관리
/// 4. UI 관리
/// </summary>
public class InGameManager : MonoBehaviour
{
    [SerializeField] private LoadLevelManager loadLevel;
    [SerializeField] private BallColl_Ctl ballColl;
    
    [SerializeField] private int life;
    [SerializeField] private int nowLevel;
    [SerializeField] private string levelName;
    [SerializeField] private float rotateSpeed;
    
    [SerializeField] private bool isPlayerDied = false;

    public event Action gameEnd;            // 게임이 끝났기 때문에 모든 기능 정지
    public event Action gameOver;           // 게임 오버 UI 이벤트
    public event Action BallGoal;           // 게임 승리 UI 이벤트
    public event Action<int> ChangeLife;    // Life 변화 감지 이벤트
    
    public int Life
    {
        get => life;
        set
        {
            life = value;

            if (life < 0)
            {
                Debug.Log("플레이어 사망!");
                life = 0;
                isPlayerDied = true;
                gameEnd?.Invoke();
                gameOver?.Invoke();
            }
            else
            {
                ChangeLife?.Invoke(life);
                Debug.Log($"벽이랑 충돌! 목숨 {Life} 남음");
            }
        }
    }
    
    public float RotateSpeed => rotateSpeed;
    
    private void OnEnable()
    {
        loadLevel = GameObject.Find("LevelLoad").GetComponent<LoadLevelManager>();
        
        loadLevel.throwLevelData += InitDatas;
        ballColl.BallColl += DecreaseLife;
        ballColl.BallCollToGoal += IsBallGoal;
        ballColl.BallOut += BallOut;
        ballColl.CameraZoom += CameraZoomTime;
    }

    private void InitDatas(Level_Datas levelDatas)
    {
        Life = levelDatas.life;
        nowLevel = levelDatas.levelNumber;
        levelName = levelDatas.levelName;
        rotateSpeed = levelDatas.rotateSpeed;

        loadLevel.throwLevelData -= InitDatas;
        Destroy(loadLevel.gameObject);
    }

    private void DecreaseLife() => Life -= 1;
    private void BallOut() => Life = -1;
    private void IsBallGoal()
    {
        if (Life == 0)
        {
            gameEnd?.Invoke();
            BallGoal?.Invoke();
            //TODO : 승리했을 시 UI 및 이펙트 관리
        }
    }

    private void CameraZoomTime(bool isZoom)
    {
        if (isZoom)
        {
            Time.timeScale = 0.3f;
            Debug.Log($"time = {Time.timeScale}");
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    #region Event 구독 해제

    private void OnDisable()
    {
        ballColl.BallColl -= DecreaseLife;
        ballColl.BallCollToGoal -= IsBallGoal;
        ballColl.BallOut -= BallOut;
        ballColl.CameraZoom -= CameraZoomTime;
    }

    #endregion
}
