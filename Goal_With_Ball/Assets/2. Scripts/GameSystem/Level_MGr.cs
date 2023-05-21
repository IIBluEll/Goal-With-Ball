using System;
using UnityEngine;

public class Level_MGr : MonoBehaviour
{
    [SerializeField] private Level_Datas levelData;

    public int NowLevel => levelData.levelNumber;
    public int Life => levelData.life;
    public string LevelName => levelData.levelName;

}
