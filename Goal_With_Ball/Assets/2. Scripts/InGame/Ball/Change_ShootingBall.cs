using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Change_ShootingBall : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 shootDir;

    [SerializeField] private bool isDrag = false;
    [SerializeField] private float shootPower = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        this.UpdateAsObservable()
            .Where(x => Input.GetMouseButtonDown(0))
            .Subscribe(x => CheckClickBall());
        
        this.UpdateAsObservable()
            .Where(x => Input.GetMouseButton(0))
            .Subscribe(x => RotateBall());
    }
    
    private void CheckClickBall()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDrag = true;
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void RotateBall()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDir = startPos - endPos;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ShootBall()
    {
        rb.AddForce(-shootDir.normalized * shootPower, ForceMode2D.Impulse);

    }
}
