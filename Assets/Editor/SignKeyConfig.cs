using UnityEditor;

[InitializeOnLoad]
public class StartUp
{
#if UNITY_EDITOR

    static StartUp()
    {
        PlayerSettings.Android.keyaliasName = "---";
        PlayerSettings.Android.keyaliasPass = "---";
        PlayerSettings.Android.keystorePass = "---";
    }

#endif
}
