using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class Fullsize_Ad : MonoBehaviour
{
#if UNITY_ANDROID
  private string adUnitId = "ca-app-pub-5066712064975468/6690406865";
#endif
    
    private InterstitialAd interstitialAd;

    [SerializeField] private AudioSource bgmSound;

    private int fullAdCount = 0;
    
    
    
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        LoadInterstitialAD();
        
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {            // 전면 광고창이 열렸을 때
            if (BGM_Mgr.instance.isQuite)
                bgmSound.volume = 0;

            fullAdCount++;
        };
        
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {   // 전면 광고가 닫혔을 때
            if (BGM_Mgr.instance.isQuite)
                bgmSound.volume = 1;
            
            LoadInterstitialAD(); // 다시 로드해서 다음 광고 대기
            
            if(fullAdCount == 0)
                Social.ReportProgress("CgkI2baKsNQcEAIQAw", 100.0f,(bool success) =>{});
            
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError aderror) =>
        {   // 전면 광고 오픈이 실패했을 때 에러 사유 디버깅과 함께 다시 로드
            Debug.LogError("광고창 오픈 실패! " + aderror);
            
            LoadInterstitialAD();
        };
    }

    public void LoadInterstitialAD()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-fullSizeAD");
        
        InterstitialAd.Load(adUnitId,adRequest, (InterstitialAd  ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("interstitial ad failed to load an ad " +
                               "with error : " + error);
                return;
            }

            interstitialAd = ad;
        });
    }
    
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("전면 광고 시작");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("전면 광고 시작 불가능");
        }
    }

    
}
