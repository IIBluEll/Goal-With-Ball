using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour
{
    public static InGameSystem instance;
    private Level_MGr levelData;
    
    [SerializeField] private Ball_Mgr playerBall;

    [SerializeField] private int life;
    [SerializeField] private int nowLevel;
    [SerializeField] private string levelName;

    [SerializeField] private bool isPlayerDied = false;
    public event Action BallDie;

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

        playerBall.CollToWall += DecreaseLife;
    }

    private void DecreaseLife() => Life -= 1;
}