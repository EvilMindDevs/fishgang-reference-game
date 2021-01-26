using Assets.Scripts.HMS.Constants;
using Assets.Scripts.HMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    class HMSManager : Singleton<HMSManager>
    {
        private static string TAG = "HMSManager";


        public HMSAccountKit accountKit;
        public HMSAdsKit adsKit;
        public HMSAnalytics analytics;
        public HMSCrash crash;
        public HMSGameService gameService;
        public HMSInAppPurchases iap;
        public HMSPushKit pushKit;
        public HMSRemoteConfig remoteConfig;

        protected override void Awake()
        {
            base.Awake();

            accountKit = new HMSAccountKit();
            adsKit = new HMSAdsKit();
            analytics = new HMSAnalytics();
            crash = new HMSCrash();
            gameService = new HMSGameService();
            iap = new HMSInAppPurchases();
            pushKit = new HMSPushKit();
            remoteConfig = new HMSRemoteConfig();
        }

        public void Init()
        {
            accountKit.Init();
            var autoLogin = PlayerPrefs.GetInt("autoLogin", 1);
            if (autoLogin == 1) {
                SignIn();
            }

            adsKit.Init();
            analytics.Init();
            crash.Init();
            remoteConfig.Init();
            pushKit.Init();
            iap.Init();
        }

        public void SignIn()
        {
            accountKit.AuthenticateUser(success =>
            {
                if (success)
                {
                     gameService.Init(accountKit.HuaweiId);
                }
                else
                {
                    Debug.Log($"{TAG} : ShowAchievementList connection is failed!");
                }
            });
            PlayerPrefs.SetInt("autoLogin", 1);
        }

        public void SignOut()
        {
            accountKit.SignOut();
            PlayerPrefs.SetInt("autoLogin", 0);
        }

        public bool CheckRemoveAds()
        {
            if (PlayerPrefs.HasKey("RestorePurchases")) {
                if (PlayerPrefs.GetInt("RestorePurchases") == 1 && iap.productPurchasedItemList.Contains(InAppPurchasesConstants.remove_ads))
                    return true;
                else
                    return false;
            } else
            {
                return false;
            }
        }

        public void CheckGamePlayTimeAchievements(int score)
        {
            if (score >= 5)
            {
                gameService.UnlockAchievement(GameServiceConstants.level_1);
            }

            if (score >= 100)
            {
                gameService.UnlockAchievement(GameServiceConstants.level_2);
            }

            if (score >= 200)
            {
                gameService.UnlockAchievement(GameServiceConstants.level_3);
            }
        }

    }
}
