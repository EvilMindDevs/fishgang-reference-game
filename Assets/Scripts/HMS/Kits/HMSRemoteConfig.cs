using HuaweiMobileServices.RemoteConfig;
using HuaweiMobileServices.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    public class HMSRemoteConfig
    {
        private RemoteConfigManager remoteConfigManager;

        string TAG = "HMSRemoteConfig";

        public void Init()
        {
            remoteConfigManager = new RemoteConfigManager();
            remoteConfigManager.GetInstance();
            Fetch();
            
        }

        public void Fetch()
        {
            remoteConfigManager.OnFecthSuccess = OnFecthSuccess;
            remoteConfigManager.OnFecthFailure = OnFecthFailure;
            remoteConfigManager.Fetch();
        }

        public void OnFecthSuccess(ConfigValues config)
        {
            remoteConfigManager.Apply(config);
            Debug.Log($"[{TAG}]: fetch() Success");
        }

        public void OnFecthFailure(HMSException exception)
        {
            Debug.Log($"[{TAG}]: fetch() Failed Error Code => {exception.ErrorCode} Message => {exception.WrappedExceptionMessage}");
        }

        public Dictionary<string, object> GetMergedAll()
        {
            return remoteConfigManager.GetMergedAll();
        }

        public void ClearAll()
        {
            remoteConfigManager.ClearAll();
        }

        public void ApplyDefault(Dictionary<string, object> dictionary)
        {
            remoteConfigManager.ApplyDefault(dictionary);
        }

        public void ApplyDefaultXml()
        {
            remoteConfigManager.ApplyDefault("xml/remoteConfig");
        }

        public ConfigValues LoadLastFetched()
        {
            return remoteConfigManager.LoadLastFetched();
        }

        public void DeveloperMode(bool val)
        {
            remoteConfigManager.SetDeveloperMode(val);
        }

        public string GetSource(string key)
        {
            return remoteConfigManager.GetSource(key);
        }

        public string GetValueAsString(string value) => remoteConfigManager.GetValueAsString(value);
    }
}