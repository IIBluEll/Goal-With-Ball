using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class ArrowRotate : MonoBehaviour
{
   [SerializeField] private float rotateSpeed = 100f;
   [SerializeField] private bool isRotate = true;

   [SerializeField] private Transform ballTrans;
   
   private void Update()
   {
      if ( isRotate == true)
      {
         Rotating();
      }
   }

   private void Rotating()
   {
      Vector2 dir = ballTrans.position - transform.position;
      
      var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      var ballRotate = Quaternion.AngleAxis(angle, Vector3.forward);
      
      transform.rotation = Quaternion.Slerp(transform.rotation, ballRotate, rotateSpeed * Time.deltaTime);

   }

   public void StopRotate()
   {
      isRotate = false;
   }
}
