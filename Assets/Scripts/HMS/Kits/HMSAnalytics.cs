using HuaweiMobileServices.Analystics;
using HuaweiMobileServices.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    public class HMSAnalytics
    {
        private static string TAG = "HMSAnalytics";

        private HiAnalyticsInstance instance;

        void InitilizeAnalyticsInstane()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

            HiAnalyticsTools.EnableLog();
            instance = HiAnalytics.GetInstance(activity);
            instance.SetAnalyticsEnabled(true);
        }

        void SendEventWithBundle(String eventID, String key, String value)
        {
            Bundle bundleUnity = new Bundle();
            bundleUnity.PutString(key, value);
            Debug.Log($"{TAG} : Analytics Kits Event Id:{eventID} Key:{key} Value:{value}");
            instance.OnEvent(eventID, bundleUnity);
        }

        public void Init()
        {
            InitilizeAnalyticsInstane();
            Debug.Log($"{TAG} : Analytics Kits Initialize");
        }

        public void SendEvent(string eventID, string key, string value)
        {
            if (string.IsNullOrEmpty(eventID) && string.IsNullOrEmpty(key) && string.IsNullOrEmpty(value))
            {
                Debug.Log($"{TAG} : Fill Fields");
            }
            else
            {
                SendEventWithBundle(eventID, key, value);
            }
        }
               

    }
}