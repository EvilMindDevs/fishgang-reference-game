using HuaweiMobileServices.Push;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    public class HMSPushKit: IPushListener
    {
        private static string TAG = "HMSPushKit";


        public Action<string> OnTokenSuccess { get; set; }
        public Action<Exception> OnTokenFailure { get; set; }

        public Action<RemoteMessage> OnMessageReceivedSuccess { get; set; }

        public void Init()
        {
            PushManager.Listener = this;
            var token = PushManager.Token;
            Debug.Log($"{TAG}: Push token from GetToken is {token}");
            if (token != null)
            {
                OnTokenSuccess?.Invoke(token);
            }
        }

        public void OnNewToken(string token)
        {
            Debug.Log($"{TAG}: Push token from OnNewToken is {token}");
            if (token != null)
            {
                OnTokenSuccess?.Invoke(token);
            }
        }

        public void OnTokenError(Exception e)
        {
            Debug.Log($"{TAG}: Error asking for Push token");
            Debug.Log(e.StackTrace);
            OnTokenFailure?.Invoke(e);
        }

        public void OnMessageReceived(RemoteMessage remoteMessage)
        {
            OnMessageReceivedSuccess?.Invoke(remoteMessage);
        }
    }
}