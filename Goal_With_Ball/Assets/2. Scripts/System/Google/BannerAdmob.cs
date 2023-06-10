using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdmob : MonoBehaviour
{
    private BannerView bannerView;

    private void Start()
    {
        MobileAds.Initialize(initStatus => {});
        
        this.RequestBanner();
        
        bannerView.Show();
    }

    private void RequestBanner()
    {
        
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5066712064975468~5963616733";
#else
        string adUnitId = "unexpected_platform";        
#endif
        
        // 이미 배너가 있을 경우 파괴
        if(this.bannerView != null)
            this.bannerView.Destroy();
        
        // 적응형 사이즈
        AdSize adaptiveSize         
            = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // 광고 다시 불러오기
        AdRequest request = new AdRequest.Builder().Build();
        
        // 배너 불러오기
        this.bannerView.LoadAd(request);
    }


}
