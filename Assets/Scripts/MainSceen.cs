using Assets.Scripts.HMS;
using Assets.Scripts.HMS.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceen : MonoBehaviour
{
    public Button store;

    // Start is called before the first frame update
    void Start()
    {
        HMSManager.Instance.Init();
    }


    void Update()
    {
        if (HMSManager.Instance.CheckRemoveAds())
        {
            HMSManager.Instance.adsKit.DestroyBannerAd();
            store.gameObject.SetActive(false);
        };
    }

    public void showAchievements() => HMSManager.Instance.gameService.ShowAchievements();
    public void showLeaderBoard() => HMSManager.Instance.gameService.ShowLeaderBoard();

    public void RemoveAds()
    {
        HMSManager.Instance.iap.BuyProduct(InAppPurchasesConstants.remove_ads);
    }


}
