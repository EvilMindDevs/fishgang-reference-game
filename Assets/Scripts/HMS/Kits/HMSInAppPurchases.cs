using Assets.Scripts.HMS.Constants;
using HmsPlugin;
using HuaweiMobileServices.IAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.HMS
{
    public class HMSInAppPurchases
    {
        private static string TAG = "HMSInAppPurchases";

        List<string> NonConsumableProducts;

        [HideInInspector]
        public int numberOfProductsRetrieved;

        public List<ProductInfo> productInfoList = new List<ProductInfo>();
        public List<string> productPurchasedList = new List<string>();
        public List<string> productPurchasedItemList = new List<string>();

        private IapManager iapManager;

        UnityEvent loadedEvent;

        public void Init()
        {
            iapManager = IapManager.GetInstance();

            NonConsumableProducts = new List<string>();
            NonConsumableProducts.Add(InAppPurchasesConstants.remove_ads);

            loadedEvent = new UnityEvent();
            iapManager.OnCheckIapAvailabilitySuccess = LoadStore;
            iapManager.OnCheckIapAvailabilityFailure = (error) =>
            {
                Debug.Log($"[{TAG}]: IAP check failed. {error.Message}");
            };
            iapManager.CheckIapAvailability();

            
        }

        public void LoadStore()
        {
            Debug.Log($"[{TAG}: LoadStore");
            RestorePurchases();
            // Set Callback for ObtainInfoSuccess
            iapManager.OnObtainProductInfoSuccess = (productInfoResultList) =>
            {
                if (productInfoResultList != null)
                {
                    foreach (ProductInfoResult productInfoResult in productInfoResultList)
                    {
                        foreach (ProductInfo productInfo in productInfoResult.ProductInfoList)
                        {
                            productInfoList.Add(productInfo);
                            Debug.Log($"{TAG} , {productInfo.ProductId} , {productInfo.ProductName} , {productInfo.Price}");
                        }
                    }
                }
                loadedEvent.Invoke();
            };

            // Set Callback for ObtainInfoFailure
            iapManager.OnObtainProductInfoFailure = (error) =>
            {
                Debug.Log($"{TAG}: IAP ObtainProductInfo failed. {error.Message}");
            };

            // Call ObtainProductInfo 
            iapManager.ObtainProductInfo(NonConsumableProducts);
        }

        public void RestorePurchases()
        {
            iapManager.OnObtainOwnedPurchasesSuccess = (ownedPurchaseResult) =>
            {
                Debug.Log($"{TAG} RestorePurchasesSuccess");

                productPurchasedList = (List<string>)ownedPurchaseResult.InAppPurchaseDataList;
                productPurchasedItemList = (List<string>)ownedPurchaseResult.ItemList;
                PlayerPrefs.SetInt("RestorePurchases", 1);
            };

            iapManager.OnObtainOwnedPurchasesFailure = (error) =>
            {
                Debug.Log($"{TAG} RestorePurchasesError" + error.Message);
                PlayerPrefs.SetInt("RestorePurchases", 0);
            };

            iapManager.ObtainOwnedPurchases();
        }

        public ProductInfo GetProductInfo(string productID)
        {
            return productInfoList.Find(productInfo => productInfo.ProductId == productID);
        }

        public void BuyProduct(string productID)
        {
            iapManager.OnBuyProductSuccess = (purchaseResultInfo) =>
            {
                Debug.Log($"{TAG}] purchaseResultInfo" + purchaseResultInfo.InAppPurchaseData);
                RestorePurchases();
                //iapManager.ConsumePurchase(purchaseResultInfo);
            };

            iapManager.OnBuyProductFailure = (errorCode) =>
            {

                switch (errorCode)
                {
                    case OrderStatusCode.ORDER_STATE_CANCEL:
                        // User cancel payment.
                        Debug.Log($"{TAG}]: User cancel payment");
                        break;
                    case OrderStatusCode.ORDER_STATE_FAILED:
                        Debug.Log($"{TAG}]: order payment failed");
                        break;

                    case OrderStatusCode.ORDER_PRODUCT_OWNED:
                        Debug.Log($"{TAG}]: Product owned");
                        break;
                    default:
                        Debug.Log($"{TAG}] BuyProduct ERROR" + errorCode);
                        break;
                }
            };

            var productInfo = productInfoList.Find(info => info.ProductId == productID);
            var payload = "test";

            iapManager.BuyProduct(productInfo, payload);
        }

        public void addListener(UnityAction action)
        {
            if (loadedEvent != null)
            {
                loadedEvent.AddListener(action);
            }
        }
    }
}