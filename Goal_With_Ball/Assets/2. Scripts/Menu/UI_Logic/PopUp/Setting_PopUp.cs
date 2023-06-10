using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting_PopUp : MonoBehaviour
{
    //================= BackGround Music =================//
    
    //================= Data Clear =================//

    public void DataClearBtn()
    {
        SaveSystem.instance.DataClear();
        SceneManager.LoadScene("MainMenu");
    }
}
