using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using UnityEngine;
using UnityEngine.UI;
using HmsPlugin;
public class AccountDemoManager : MonoBehaviour
{

    private const string NOT_LOGGED_IN = "No user logged in";
    private const string LOGGED_IN = "{0} is logged in";
    private const string LOGIN_ERROR = "Error or cancelled login";

    private AccountManager accountManager;

    void Awake()
    {
        accountManager = AccountManager.GetInstance();
        accountManager.OnSignInSuccess = OnLoginSuccess;
        accountManager.OnSignInFailed = OnLoginFailure;
    }

    void Start()
    {
        LogIn();
    }

    public void LogIn()
    {
        accountManager.SignIn();
    }

    public void LogOut()
    {
        accountManager.SignOut();
    }

    public void OnLoginSuccess(AuthHuaweiId authHuaweiId)
    {
        Debug.Log("[HMS]: RESULT AUTHSERVICE => " + authHuaweiId.DisplayName);
    }

    public void OnLoginFailure(HMSException error)
    {
        Debug.Log("[HMS]: RESULT AUTHSERVICE => " + LOGIN_ERROR);
    }
}
