using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Make_LevelData : MonoBehaviour
{
    [SerializeField] private int numberOfLevels = 30;
    
    void Start()
    {
        CreateLevelDataObjects();
    }

    private void CreateLevelDataObjects()
    {
#if UNITY_EDITOR
        for (int i = 1; i <= numberOfLevels; i++)
        {
            Level_Datas data = ScriptableObject.CreateInstance<Level_Datas>();

            data.levelNumber = i;
            data.life = (i - 1) / 10 + 2;
            data.levelName = $"Level {i}";
            data.rotateSpeed = 50 * data.life;
            AssetDatabase.CreateAsset(data, "Assets/3. Datas/LevelData/"+ "Level " + i  + ".asset");
        }

        AssetDatabase.SaveAssets();
#endif
    }
}
