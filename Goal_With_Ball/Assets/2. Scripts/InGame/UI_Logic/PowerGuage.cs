using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 1. 공의 발사 파워를 결정하는 슬라이더
/// 2. Event Trigger를 활용해 버튼을 눌렀을 때 / 뗄 경우로 이벤트 전달
/// </summary>

public class PowerGuage : MonoBehaviour
{
    private EventTrigger powerBtnEvent;
    private Button powerBtn;

    private Coroutine PowerCoroutine;
    
    public event Action btnOnClick;     // => 버튼이 눌렸을 때
    public event Action<float> btnClickEnd;    // => 버튼 눌림이 끝났을 때

    [SerializeField] private Slider powerGauge;
    public float power = 0;
    
    private void Start()
    {
        powerBtn = this.GetComponent<Button>();
        powerBtnEvent = this.GetComponent<EventTrigger>();
    }

    public void PowerBtn_ONClick()
    {
        if (PowerCoroutine == null)
        {
            PowerCoroutine = StartCoroutine(PowerGaugeChange());
            btnOnClick?.Invoke();
        }
    }

    public void PowerBtn_MouseUP()
    {
        if (PowerCoroutine != null)
        {
            StopCoroutine(PowerCoroutine);
            PowerCoroutine = null;
        }

        btnClickEnd?.Invoke(power);
        
        powerBtn.interactable = false;
        Destroy(powerBtnEvent);
    }
    
    IEnumerator PowerGaugeChange()
    {
        var duration = 0.01f;
        var guageChange = 0.1f;
        
        while (powerGauge.value <= 9.5f)
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
        PowerCoroutine = StartCoroutine(PowerGaugeChange());
    }
}
