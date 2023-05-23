using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BallRotate : MonoBehaviour
{
    [SerializeField] private PowerBtn powerBtn;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isRotate = true;
 
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootPower;
    
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rotateSpeed = InGameSystem.instance.RotateSpeed;
        
        this.UpdateAsObservable()
            .TakeWhile(x => isRotate)
            .Subscribe(x => RotateBall());
    }

    private void RotateBall() 
            => transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);


    public void StopRotateBall()
            => isRotate = false;
    
    
    public void ShootBall()
    {
        var power = powerBtn.power;
        
        Vector2 direction = shootPoint.position - transform.position;
        rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        
        Debug.Log(power);
    }
}
