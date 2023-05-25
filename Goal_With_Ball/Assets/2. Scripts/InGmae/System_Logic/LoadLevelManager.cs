using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 1. 스테이지 선택 씬에서 플레이어가 스테이지를 선택할 경우
/// 2. 해당 씬 정보가 담긴 ScriptableObject를 받음
/// 3. DonDestroy를 통해 인게임 씬에서 게임매니저에게 정보를 넘김
/// </summary>

public class LoadLevelManager : MonoBehaviour
{
    public Level_Datas levelDatas;

    public event Action<Level_Datas> throwLevelData;
    
    private void Start()
    {
        SceneManager.sceneLoaded += CheckInGameScene;
        DontDestroyOnLoad(this.gameObject);
    }

    private void CheckInGameScene(Scene scene, LoadSceneMode mode)
    {
        throwLevelData?.Invoke(levelDatas);
    }

    #region MyRegion

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckInGameScene;
    }

    #endregion
}
