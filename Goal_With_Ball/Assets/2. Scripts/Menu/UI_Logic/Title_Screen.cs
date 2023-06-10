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

    [SerializeField] private GameObject shopPopUp;
    
    [SerializeField] private GameObject titleUI;

    [SerializeField] private List<GameObject> clearIcon;

    private void Start()
    {
        var nowlevel = SaveSystem.instance.saveData.nowStage;
        var nextlevel = SaveSystem.instance.saveData.nextStage;
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

    public void GoBack_Btn()
    {
        titleUI.SetActive(true);
    }

    public void Shop_Btn()
    {
        shopPopUp.SetActive(true);
    }

    public void QuitGameBtn()
    {
#if UNITY_ANDROID
        Application.Quit();
#endif
    }
    
    //==========Debug================//
    
    
}
