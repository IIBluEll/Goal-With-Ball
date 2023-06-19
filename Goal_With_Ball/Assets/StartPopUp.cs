using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPopUp : MonoBehaviour
{
    [SerializeField] private GameObject descriptionPopUp;
    [SerializeField] private GameObject alarmPopUP;
    
    public void YesBtn()
    {
        descriptionPopUp.SetActive(true);    
        this.gameObject.SetActive(false);
    }

    public void NoBtn()
    {
        alarmPopUP.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
