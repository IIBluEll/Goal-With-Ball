using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DragBall : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform point;
    [SerializeField] private Transform Ballpoint;
    private Vector3 originPosition; //최초 클릭 위치 
    private bool isMouseClick = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseClick = true;
            originPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isMouseClick = false;

            Vector2 direction = point.position - Ballpoint.position;
            rb.AddForce(direction.normalized * 5f, ForceMode2D.Impulse);

        }

        if (isMouseClick)
        {
            DraggingBall();

            
        }
        
    }

    private void DraggingBall()
    {
        var nowMousePose = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diffVector = (nowMousePose - originPosition) * -1; // 드래그 한 벡터 역방향
        var diffVectorManitude = diffVector.magnitude;
        var diffNormalVec = diffVector.normalized;

        var rotZ = Mathf.Atan2(diffNormalVec.y, diffNormalVec.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(rotZ,Vector3.forward);
    }
}
