using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataObject", order = 1)]
public class Level_Datas : ScriptableObject
{
    public int levelNumber;
    public int life;
    public float rotateSpeed;
    
    public string levelName;
    
}
