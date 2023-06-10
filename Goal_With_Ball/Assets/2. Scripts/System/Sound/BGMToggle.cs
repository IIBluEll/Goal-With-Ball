using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMToggle : MonoBehaviour
{
    private Toggle bgmToggle;
    
    private void Start()
    {
        bgmToggle = GetComponent<Toggle>();

        bgmToggle.isOn = SaveSystem.instance.saveData.isBgmOn;
    }

    public void ChangeToggle()
    {
        SaveSystem.instance.saveData.isBgmOn = bgmToggle.isOn;
        SaveSystem.instance.SaveGameData();
        
        BGM_Mgr.instance.BgmToggle(bgmToggle.isOn);
    }
}
