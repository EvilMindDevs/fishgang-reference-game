using HuaweiMobileServices.Crash;
using UnityEngine;

public class CrashManager : MonoBehaviour
{
    IAGConnectCrash agConnectCrash;

    string TAG = "CrashManager";

    void Awake()
    {
        Debug.Log($"[{TAG}]: Crash Initialized");
        agConnectCrash = AGConnectCrash.GetInstance();
    }

    void Start()
    {
        enableCrashCollection(true);
    }

    public void enableCrashCollection(bool value)
    {
        agConnectCrash.EnableCrashCollection(value);
        Debug.Log($"[{TAG}: Crash enableCrashCollection {value}");
    }

    public void testIt()
    {
        Debug.Log($"[{TAG}: Crash testIt");
        Application.ForceCrash(0);
    }

    enum Log
    {
        DEBUG=3,
        INFO=4,
        WARN=5,
        ERROR=6,
    }

    public void customReport()
    {
        agConnectCrash.SetUserId("testuser");
        agConnectCrash.Log((int) Log.DEBUG, "set debug log.");
        agConnectCrash.Log((int) Log.INFO, "set info log.");
        agConnectCrash.Log((int) Log.WARN, "set warning log.");
        agConnectCrash.Log((int) Log.ERROR, "set error log.");
        agConnectCrash.SetCustomKey("stringKey", "Hello world");
        agConnectCrash.SetCustomKey("booleanKey", false);
        agConnectCrash.SetCustomKey("doubleKey", 1.1);
        agConnectCrash.SetCustomKey("floatKey", 1.1f);
        agConnectCrash.SetCustomKey("intKey", 0);
        agConnectCrash.SetCustomKey("longKey", 11L);
        Debug.Log("[HMS]: Crash customReport");
    }
}
