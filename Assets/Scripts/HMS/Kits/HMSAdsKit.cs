using HmsPlugin;
using HuaweiMobileServices.Ads;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    public class HMSAdsKit
    {
        private static string TAG = "HMSAdsKit";

        private const string BANNER_AD_ID = "testw6vs28auh3";

        private BannerAdsManager bannerAdsManager;

        public void Init()
        {
            Debug.Log($"{TAG} Init");
            InitBannerAds();
        }

        // Banner Ad
        public void InitBannerAds()
        {
            bannerAdsManager = BannerAdsManager.GetInstance();
            bannerAdsManager.AdId = BANNER_AD_ID;
        }

        public void ShowBannerAd()
        {
            bannerAdsManager.ShowBannerAd();
        }

        public void DestroyBannerAd()
        {
            bannerAdsManager.DestroyBannerAd();
        }

        public void HideBannerAd()
        {
            bannerAdsManager.HideBannerAd();
        }

    }
}