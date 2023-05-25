using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_StageButton : MonoBehaviour
{
    [SerializeField] private Level_Datas levels;

    public void OnClick()
    {
        var newObj = new GameObject().AddComponent<LoadLevelManager>();
        newObj.levelDatas = levels;
        newObj.name = "LevelLoad";
    
        TempSceneChange();
    }

    private void TempSceneChange()
    {
        //ToDo : 비동기 씬 전환 만들기
        SceneManager.LoadScene("InGame");
    }
}
