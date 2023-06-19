using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
/// <summary>
/// 1. 게임이 끝났을 때 승리 또는 패배 UI 관리
/// 2. 일시정지 시 UI 관리
/// </summary>

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private InGameManager ingameMgr;
    [SerializeField] private LoadLevelManager loadLevel;

    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject pauseUI;

    [SerializeField] private Button nextStageBtn;

    [SerializeField] private Level_Datas nextlevelDatas;
    [SerializeField] private Level_Datas nowlevelDatas;

    [SerializeField] private AudioClip victoryAudioClip;
    [SerializeField] private AudioClip loseAudioClip;

    [SerializeField] private int endLevel;
    
    private void OnEnable()
    {
        loadLevel = GameObject.Find("LevelLoad").GetComponent<LoadLevelManager>();
        loadLevel.throwLevelData += InitData;

        ingameMgr.BallGoal += GameVictory;
        ingameMgr.gameOver += GameLose;
    }

    private void InitData(Level_Datas levelData)
    {
        var nowpath = $"Level {levelData.levelNumber}";
        var nextpath = $"Level {levelData.levelNumber + 1}";
        
        nowlevelDatas =  Resources.Load<Level_Datas>(nowpath);
        nextlevelDatas = Resources.Load<Level_Datas>(nextpath);
        
        Debug.Assert(nowlevelDatas,"GameEndUI에서 nowlevelData값이 안들어옴");
        Debug.Assert(nextlevelDatas,"GameEndUI에서 nextlevelData값이 안들어옴");
    }

    private void GameSave(int changeV)
    {
        if (nowlevelDatas.levelNumber > SaveSystem.instance.saveData.nowStage)
        {   // 이미 클리어한 스테이지를 다시 플레이했을 때 레벨 오름을 방지하기 위함
            SaveSystem.instance.saveData.nowStage += changeV;
            SaveSystem.instance.saveData.nextStage += changeV;
        }
        
        SaveSystem.instance.SaveGameData();
    }

    private void GameVictory()
    {
        if (nowlevelDatas.levelNumber == endLevel)
            nextStageBtn.interactable = false;
        
        victoryUI.SetActive(true);
        BGM_Mgr.instance.ChangeBgm(victoryAudioClip);

        if (nowlevelDatas.levelNumber == 1)
        {
            Social.ReportProgress("CgkI2baKsNQcEAIQAQ",100.0f,(bool success)=>{});
        }
        else if (nowlevelDatas.levelNumber == 2)
        {
            Social.ReportProgress("CgkI2baKsNQcEAIQAg",100.0f,(bool success)=>{});
        }
    }

    private void GameLose()
    {
        LoseUI.SetActive(true);
        BGM_Mgr.instance.ChangeBgm(loseAudioClip);
    }
    
    //==========Victory Btn============//

    public void NextStageBtn()
    {
        var newObj = new GameObject().AddComponent<LoadLevelManager>();
        newObj.levelDatas = nextlevelDatas;
        newObj.name = "LevelLoad";
        
        GameSave(1);
        
        SceneManager.LoadScene("InGame");
    }

    public void MainMenuV()
    {
        GameSave(1);
        
        SceneManager.LoadScene("MainMenu");
    }
    
    //==========Lose Btn============//

    public void RertyBtn()
    {
        var newObj = new GameObject().AddComponent<LoadLevelManager>();
        newObj.levelDatas = nowlevelDatas;
        newObj.name = "LevelLoad";
        
        GameSave(0);
        
        SceneManager.LoadScene("InGame");
    }
    
    public void MainMenuL()
    {
        GameSave(0);
        
        SceneManager.LoadScene("MainMenu");
    }
    
    //============Pause UI==================//

    public void PauseBtn()
    {
        pauseUI.SetActive(true);

        Time.timeScale = 0;
    }

    public void ResumeBtn()
    {
        pauseUI.SetActive(false);
        
        Time.timeScale = 1;
    }

    public void MenuBtn()
    {
        Time.timeScale = 1;
        MainMenuL();
    }
}
