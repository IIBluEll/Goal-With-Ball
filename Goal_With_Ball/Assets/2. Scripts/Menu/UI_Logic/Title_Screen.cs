using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Screen : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    
    [SerializeField] private GameObject settingPopUp;
    [SerializeField] private GameObject quitPopUp;
    [SerializeField] private GameObject alarmPopUp;

    [SerializeField] private List<GameObject> clearIcon;

    // Debug
    [SerializeField] private Text debugText;

    private void Start()
    {
        var nowlevel = SaveSystem.instance.saveData.nowStage;
        var nextlevel = SaveSystem.instance.saveData.nextStage;

        // Debug
        debugText.text = $"<Debug> now : {nowlevel} /// next : {nextlevel}";
    }

    public void PopDown()
    {
        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }
    
    public void Setting_Btn()
    {
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
        
        settingPopUp.SetActive(true);
    }

    public void Quit_Btn()
    {
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
        
        quitPopUp.SetActive(true);
    }

    public void Alarm_Btn()
    {
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
        
        alarmPopUp.SetActive(true);
    }
    
    
    //==========Debug================//
    
    
}
