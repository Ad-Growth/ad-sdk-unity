using UnityEngine;

[System.Serializable]
public class AdServerSettings
{
    public string AdServerKey = "12345678-aaaa-xxxx-yyyy-zzzzzzzzzzzz";
    public string AdServerGoogleKey = "ca-app-pub-3940256099942544~3347511713";
    public string AdSdkVersion = "1.1.+";
    public bool AutoUpdateManifest = true;
    public bool AutoUpdateBuildGradle = true;
    public bool AutoUpdateSettingsGradle = true;
    public bool AutoUpdateGradleProperties = true;
    public bool Changed = false;

    public void Save()
    {
        PlayerPrefs.SetString("AdServerKey", AdServerKey);
        PlayerPrefs.SetString("AdServerGoogleKey", AdServerGoogleKey);
        PlayerPrefs.SetString("AdSdkVersion", AdSdkVersion);
        PlayerPrefs.SetInt("AdAutoUpdateManifest", AutoUpdateManifest ? 1 : 0);
        PlayerPrefs.SetInt("AdAutoUpdateBuildGradle", AutoUpdateBuildGradle ? 1 : 0);
        PlayerPrefs.SetInt("AdAutoUpdateSettingsGradle", AutoUpdateSettingsGradle ? 1 : 0);
        PlayerPrefs.SetInt("AdAutoUpdateGradleProperties", AutoUpdateGradleProperties ? 1 : 0);
        Changed = false;
        PlayerPrefs.Save();

        OnSettingsChanged?.Invoke(this);
    }

    public static AdServerSettings Load()
    {
        AdServerSettings settings = new AdServerSettings();
        settings.AdServerKey = PlayerPrefs.GetString("AdServerKey", settings.AdServerKey);
        settings.AdServerGoogleKey = PlayerPrefs.GetString("AdServerGoogleKey", settings.AdServerGoogleKey);
        settings.AdSdkVersion = PlayerPrefs.GetString("AdSdkVersion", settings.AdSdkVersion);
        settings.AutoUpdateManifest = PlayerPrefs.GetInt("AdAutoUpdateManifest", settings.AutoUpdateManifest ? 1 : 0) == 1;
        settings.AutoUpdateBuildGradle = PlayerPrefs.GetInt("AdAutoUpdateBuildGradle", settings.AutoUpdateBuildGradle ? 1 : 0) == 1;
        settings.AutoUpdateSettingsGradle = PlayerPrefs.GetInt("AdAutoUpdateSettingsGradle", settings.AutoUpdateSettingsGradle ? 1 : 0) == 1;
        settings.AutoUpdateGradleProperties = PlayerPrefs.GetInt("AdAutoUpdateGradleProperties", settings.AutoUpdateGradleProperties ? 1 : 0) == 1;
        return settings;
    }

    public delegate void SettingsChangedHandler(AdServerSettings settings);
    public static event SettingsChangedHandler OnSettingsChanged;
}
