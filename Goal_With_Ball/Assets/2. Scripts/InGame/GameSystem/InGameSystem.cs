using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour
{
    public static InGameSystem instance;
    private Level_MGr levelData;
    
    [SerializeField] private Ball_Mgr playerBall;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private int life;
    [SerializeField] private int nowLevel;
    [SerializeField] private string levelName;

    [SerializeField] private bool isPlayerDied = false;
    public event Action BallDie;
    public event Action ChangeLife;

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
                BallDie?.Invoke();
            }
            else
            {
                ChangeLife?.Invoke();
                Debug.Log($"벽이랑 충돌! 목숨 {Life} 남음");
            }
        }
    }
    private void OnEnable()
    {
        instance = this;
        levelData = GameObject.Find("LevelMgr").GetComponent<Level_MGr>();
        
        life = levelData.Life;
        nowLevel = levelData.NowLevel;
        levelName = levelData.LevelName;
        
        Destroy(levelData.gameObject);

        InitPosition(nowLevel);
    }

    private void InitPosition(int nowLevel)
    {
        var whereToGo = GameObject.Find("Level " + nowLevel).transform.position;
        var cameraPos = new Vector3(whereToGo.x, whereToGo.y, -10);
        
        playerBall.transform.position = whereToGo;
        mainCamera.transform.position = cameraPos;
    }

    public void DecreaseLife() => Life -= 1;
}