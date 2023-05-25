using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class ShootingBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject arrow;
    
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Vector2 shootDirection;
    
    [SerializeField]private bool isDragging = false;
    [SerializeField]private float shootForce = 5f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        this.UpdateAsObservable()
            .Where(x => Input.GetMouseButtonDown(0))
            .Subscribe(x => CheckClickBall(), ()=> Debug.Log("종료"));
        
        this.UpdateAsObservable()
            .Where(x => isDragging && Input.GetMouseButtonUp(0))
            .Subscribe(x => ShootBall());
    }
    
    private void CheckClickBall()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            arrow.SetActive(true);
            isDragging = true;
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void ShootBall()
    {
        arrow.SetActive(false);
        isDragging = false;
        dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = dragStartPosition - dragEndPosition;
        rb.AddForce(shootDirection.normalized * shootForce, ForceMode2D.Impulse);
    }
}
