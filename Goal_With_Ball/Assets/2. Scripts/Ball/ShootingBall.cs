using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private float forceMag = 10f;
    [SerializeField] private bool isMove = false;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
#if UNITY_EDITOR
        
        this.UpdateAsObservable()
            .FirstOrDefault(x => isMove == false && Input.GetKeyDown(KeyCode.Space))
            .Subscribe(x => ShootBall(), ()=> Debug.Log("종료"));
#endif
    }
    
    private void ShootBall()
    {
        isMove = true;

        var angle = UnityEngine.Random.Range(0f, 360f);
        var radian = angle * Mathf.Deg2Rad;
        var forceDir = new Vector2(Mathf.Cos(radian), Mathf.Sign(radian));

        rb.AddForce(forceDir * forceMag, ForceMode2D.Impulse);
    }
}
