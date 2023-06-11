using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionCam : MonoBehaviour
{
    //private Camera thiscam;
    private void OnEnable() // 해상도 설정
    {
        var cam = GetComponent<Camera>();

        // 카메라 컴포넌트의 Viewport Rect
        var rt = cam.rect;

        // 현재 세로 모드 9:16, 반대로 하고 싶으면 16:9를 입력.
        var scale_height = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (가로 / 세로)
        var scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        cam.rect = rt;
        
        // thiscam = GetComponent<Camera>();
        //
        // var setWidth = 1080;
        // var setHeight = 1920;
        //
        // var deviceWidth = Screen.width;
        // var deviceHeight = Screen.height;
        //
        // UnityEngine.Device.Screen.SetResolution(setWidth,(int)(((float)deviceHeight / deviceWidth) * setWidth),true);
        //
        // if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)    // 기기 해상도가 더 클 경우
        // {
        //     var newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
        //     thiscam.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);    // 새로운 Rect 적용
        // }
        // else // 게임의 해상도 비가 더 큰 경우
        // {
        //     var newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
        //     thiscam.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);  // 새로운 Rect 적용
        // }
    }
}
