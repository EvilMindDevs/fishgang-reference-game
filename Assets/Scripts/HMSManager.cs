using HmsPlugin;
using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.RemoteConfig;
using HuaweiMobileServices.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HMSManager : MonoBehaviour
    {

        public Button storeIAP;

        string TAG = "RefGame";

        void Start()
        {
            HMSAdsKitManager.Instance.ShowBannerAd();
            
            void OnCheckIapAvailabilitySuccess()
            {
                Debug.Log($"{TAG} Iap is available");
                CheckRemoveAds();
            }
            HMSIAPManager.Instance.OnCheckIapAvailabilitySuccess = OnCheckIapAvailabilitySuccess;
            HMSIAPManager.Instance.CheckIapAvailability();

            RemoteConfig();
        }

        void Update()
        {

        }

        private void RemoteConfig()
        {
            void OnFecthSuccess(ConfigValues config)
            {
                HMSRemoteConfigManager.Instance.Apply(config);
                Debug.Log($"{TAG}: fetch() sucess {HMSRemoteConfigManager.Instance.GetValueAsString("version")}");
                GameObject.Find("Version").GetComponent<Text>().text = $"version : {HMSRemoteConfigManager.Instance.GetValueAsString("version")}";
            }

            void OnFecthFailure(HMSException exception)
            {
                Debug.Log($"{TAG}: fetch() Failed Error Code => {exception.ErrorCode} Message => {exception.WrappedExceptionMessage}");
            }

            HMSRemoteConfigManager.Instance.OnFecthSuccess = OnFecthSuccess;
            HMSRemoteConfigManager.Instance.OnFecthFailure = OnFecthFailure;
            HMSRemoteConfigManager.Instance.Fetch();
        }

        public void ShowAchievements() => HMSAchievementsManager.Instance.ShowAchievements();

        public void ShowLeaderBoard() => HMSLeaderboardManager.Instance.ShowLeaderboards();

        public void RemoveAds()
        {
            void OnBuyProductSuccess(PurchaseResultInfo purchaseResultInfo)
            {
                Debug.Log($"{TAG} Purchase success");
                CheckRemoveAds();
            }

            void OnBuyProductFailure(int resultCode)
            {
                Debug.Log($"{TAG} Purchase failed result code " + resultCode);
            }

            HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
            HMSIAPManager.Instance.OnBuyProductFailure = OnBuyProductFailure;
            HMSIAPManager.Instance.BuyProduct(HMSIAPConstants.removeAds, false);
        }

        private void CheckRemoveAds()
        {
            HMSIAPManager.Instance.RestorePurchases((restoredProducts) =>
            {
                foreach (var item in restoredProducts.ItemList)
                {
                    if(item == HMSIAPConstants.removeAds)
                    {
                        HMSAdsKitManager.Instance.HideBannerAd();
                        storeIAP.gameObject.SetActive(false);
                        Debug.Log($"{TAG} , remove_ads Active");
                    }
                }
            });
        }

    }
}