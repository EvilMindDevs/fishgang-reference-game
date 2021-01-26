using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiMobileServices.Analystics;
using HuaweiMobileServices.Utils;
using UnityEngine.UI;
using System.Net.Mail;

namespace HmsPlugin
{
    public class AnalyticsDemoManager: MonoBehaviour
    {
        string TAG = "AnalyticsDemoManager";

        private AnalyticsManager analyticsManager;
        InputField eventID, key, value;

        void InitilizeAnalyticsInstane()
        {
            eventID = GameObject.Find("EventId").GetComponent<InputField>();
            key = GameObject.Find("Param1").GetComponent<InputField>();
            value = GameObject.Find("Param2").GetComponent<InputField>();

        }
        public void SendEvent()
        {
            SendEvent(eventID.text, key.text, value.text);
        }
        void SendEvent(string eventID, string key, string value)
        {
            if(string.IsNullOrEmpty(eventID) && string.IsNullOrEmpty(key) && string.IsNullOrEmpty(value))
            {
                Debug.Log($"[{TAG}: Fill Fields"); 
            }
            else
            {
                analyticsManager.SendEventWithBundle(eventID, key, value);
            }
        }

        void Start()
        {
            InitilizeAnalyticsInstane();
            analyticsManager = AnalyticsManager.GetInstance();
        }

        /*private void Start()
        {
            SendEvent("123", "1", "2");
        }*/


        // Update is called once per frame
        void Update()
        {

        }
    }
}

