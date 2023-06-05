using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private string GameDataFileName = "BallData.json";

    public class SaveData
    {
        public int nowStage;
        public int nextStage;
    }

    public SaveData _SaveData;
    public SaveData saveData
    {
        get
        {
            if (_SaveData == null)
            {
                LoadGameData();
                SaveGameData();
            }

            return _SaveData;
        }
    }
    
    private void OnEnable()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        LoadGameData();
        SaveGameData();
    }

    public void LoadGameData()
    {
        var filePath = Path.Combine(Application.persistentDataPath, GameDataFileName);

        if (File.Exists(filePath))
        {
            print("데이터 불러오기 성공");
            var fromJasonData = File.ReadAllText(filePath);
            _SaveData = JsonUtility.FromJson<SaveData>(fromJasonData);
        }
        else
        {
            print("새로운 저장 파일 생성");
            _SaveData = new SaveData();
            saveData.nextStage = 1;
            saveData.nowStage = 0;
        }
    }

    public void SaveGameData()
    {
        var toJasonData = JsonUtility.ToJson(saveData);
        var filePath = Path.Combine(Application.persistentDataPath, GameDataFileName);
        
        File.WriteAllText(filePath,toJasonData);
        
        print("저장 완료");
        print($"저장 위치 : {filePath}");
        print($"현재 레벨 : {saveData.nowStage}");
        print($"다음 레벨 : {saveData.nextStage}");
    }
    
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveGameData();
        }
    }
}