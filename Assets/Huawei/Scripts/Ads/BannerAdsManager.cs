﻿using HuaweiConstants;
using HuaweiMobileServices.Ads;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Video;
using static HuaweiConstants.UnityBannerAdPositionCode;

namespace HmsPlugin
{
    public class BannerAdsManager : MonoBehaviour
    {
        private const string TAG = "BannerAdsManager";

        public event Action BannerLoaded;
        public event Action BannerFailedToLoad;
        public static BannerAdsManager GetInstance(string name = "AdsManager") => GameObject.Find(name).GetComponent<BannerAdsManager>();
        private BannerAd bannerAdView = null;

        private bool _isInitialized;
        AdStatusListener mAdStatusListener;
        private string mAdId;
        public string AdId
        {
            get => mAdId;
            set
            {
                Debug.Log($"[{TAG}] BannerAdManager: Set banner ads ID: {value}");
                mAdId = value;
            }
        }

        public void LoadBannerAds(UnityBannerAdPositionCodeType position, string bannerSize = UnityBannerAdSize.BANNER_SIZE_320_50)
        {
            if (!_isInitialized) return;

            Debug.Log($"[{TAG}] BannerAdManager LoadBanner Ads : ");

            mAdStatusListener = new AdStatusListener();
            mAdStatusListener.mOnAdLoaded += onAdLoadSuccess;
            mAdStatusListener.mOnAdClosed += onAdLoaClosed;
            mAdStatusListener.mOnAdImpression += onAdLoadImpression;
            mAdStatusListener.mOnAdClicked += mOnAdClicked;
            mAdStatusListener.mOnAdOpened += mOnAdOpened;
            mAdStatusListener.mOnAdFailed += mOnAdFailed;

            bannerAdView = new BannerAd(mAdStatusListener);
            bannerAdView.AdId = mAdId;
            bannerAdView.PositionType = (int)position;
            bannerAdView.SizeType = bannerSize;
            bannerAdView.AdStatusListener = mAdStatusListener;
            bannerAdView.LoadBanner(new AdParam.Builder().Build());


        }

        private void onAdLoadSuccess(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager onAdLoadSuccess : ");

            BannerLoaded?.Invoke();
        }

        private void onAdLoaClosed(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager onAdLoaClosed : ");
        }

        private void onAdLoadImpression(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager onAdLoadImpression : ");
        }

        private void mOnAdClicked(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager mOnAdClicked : ");
        }

        private void mOnAdFailed(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager mOnAdFailed : ");

            BannerFailedToLoad?.Invoke();
        }

        private void mOnAdOpened(object sender, EventArgs e)
        {
            Debug.Log($"[{TAG}] BannerAdManager mOnAdOpened : ");
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"[{TAG}] BannerAdManager Start");
            HwAds.Init();
            _isInitialized = true;

            LoadBannerAds(UnityBannerAdPositionCodeType.POSITION_BOTTOM);
            //HideBannerAd();
        }

        public void ShowBannerAd()
        {
            if (bannerAdView == null)
            {
                Debug.Log($"[{TAG}] Banner Ad is NULL");
                return;
            }

            bannerAdView.ShowBanner();
        }

        public void HideBannerAd()
        {
            if (bannerAdView == null)
            {
                Debug.Log($"[{TAG}] Banner Ad is NULL");
                return;
            }
            bannerAdView.HideBanner();
        }

        public void DestroyBannerAd()
        {
            if (bannerAdView == null)
            {
                Debug.Log($"[{TAG}] Banner Ad is NULL");
                return;
            }
            bannerAdView.DestroyBanner();
        }
    }

}