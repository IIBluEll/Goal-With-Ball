using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopUpUI : MonoBehaviour
{
   [SerializeField] private string url;

   public void OpenURL()
   {
#if UNITY_ANDROID
       Application.OpenURL(url);
#else
       Debug.Log("Opening website: " + url);

#endif
   }
}
