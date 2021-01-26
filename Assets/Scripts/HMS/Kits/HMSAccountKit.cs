using HuaweiMobileServices.Id;
using System;
using UnityEngine;

public class HMSAccountKit
{
    private static string TAG = "HMSAccountKit";

    private HuaweiIdAuthService _authService;
    public AuthHuaweiId HuaweiId;

    public CommonAuthUser commonAuthUser = null;

    public void Init()
    {
        InitHuaweiAuthService();
    }

    public bool IsAuthenticated()
    {
        return commonAuthUser != null;
    }

    private void InitHuaweiAuthService()
    {
        if (_authService == null)
        {
            var authParams = new HuaweiIdAuthParamsHelper(HuaweiIdAuthParams.DEFAULT_AUTH_REQUEST_PARAM_GAME).SetIdToken().SetId().CreateParams();
            _authService = HuaweiIdAuthManager.GetService(authParams);
            Debug.Log($"{TAG} authservice is assigned.");
        }
    }

    public void SignOut()
    {
        commonAuthUser = null;
        _authService.SignOut();
    }

    public void AuthenticateUser(Action<bool> callback = null)
    {
        if (!IsAuthenticated())
        {
            _authService.StartSignIn(authId =>
            {
                Debug.Log($"{TAG} : Signed In Succesfully!");

                commonAuthUser = new CommonAuthUser
                {
                    email = authId.Email,
                    name = authId.DisplayName,
                    id = authId.OpenId,
                    photoUrl = authId.AvatarUriString
                };

                PlayerPrefs.SetInt("autoLogin", 1);

                HuaweiId = authId;
                callback?.Invoke(true);
            }, (error) =>
            {
                commonAuthUser = null;
                callback?.Invoke(false);
            });
        }
    }

}
