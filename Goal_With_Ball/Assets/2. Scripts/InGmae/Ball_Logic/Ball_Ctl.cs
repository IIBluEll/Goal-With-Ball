using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

// 1. 공, 화살표 회전
// 2. 슈팅
public class Ball_Ctl : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private PowerGuage powerGuage;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private InGameManager inGameManager;
    [SerializeField] private GameObject arrow;
    
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isRotate = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rotateSpeed = inGameManager.RotateSpeed;
        
        this.UpdateAsObservable()
            .TakeWhile(x => isRotate)
            .Subscribe(x => RotateBall());

        powerGuage.btnOnClick += StopRotateBall;
        powerGuage.btnClickEnd += ShootBall; // power 매개변수 전달됨
    }
    
    private void RotateBall() 
        => transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);


    public void StopRotateBall()
        => isRotate = false;
    
    
    public void ShootBall(float power)
    {
        //var power = powerGuage.power;
        
        arrow.SetActive(false);
        Vector2 direction = shootPoint.position - transform.position;
        rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        
        Debug.Log(power);
    }

    #region Event 구독 해제

    private void OnDisable()
    {
        powerGuage.btnOnClick -= StopRotateBall;
        powerGuage.btnClickEnd -= ShootBall;
    }

    #endregion
}
