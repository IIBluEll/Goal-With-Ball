using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerBtn : MonoBehaviour
{
    [SerializeField] private BallRotate ball;
    
    [SerializeField] public float power = 0;
    
    [SerializeField] private Slider powerGauge;
    private Button powerBtn;
    private EventTrigger thisEvent;

    private Coroutine PowerCoroutine;

    private void Start()
    {
        powerBtn = GetComponent<Button>();
        thisEvent = GetComponent<EventTrigger>();
    }

    public void PowerBtn_OnClik()
    {
        if (PowerCoroutine == null)
            PowerCoroutine = StartCoroutine(PowerGaugeChange());
        
    }

    public void PowerBtn_MouseUp()
    {
        if (PowerCoroutine != null)
        {
            StopCoroutine(PowerCoroutine);
            PowerCoroutine = null;
        }

        powerBtn.interactable = false;
        Destroy(thisEvent);
    }

    IEnumerator PowerGaugeChange()
    {
        var duration = 0.05f;
        var guageChange = 0.5f;
        
        while (powerGauge.value <= 19.5f)
        {
            powerGauge.value += guageChange;
            power = powerGauge.value;

            yield return new WaitForSeconds(duration);
        }

        while (powerGauge.value > 1.5f)
        {
            powerGauge.value -= guageChange;
            power = powerGauge.value;
                
            yield return new WaitForSeconds(duration);
        }
        StartCoroutine(PowerGaugeChange());
    }
}
