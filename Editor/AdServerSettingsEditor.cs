using UnityEngine;
using UnityEditor;

public class AdServerSettingsEditor : EditorWindow
{
    private static AdServerSettings settings;

    [MenuItem("Window/AdServer Settings")]
    public static void ShowWindow()
    {
        GetWindow<AdServerSettingsEditor>("AdServer Settings");
    }

    private void OnEnable()
    {
        if (settings == null)
        {
            try { settings = AdServerSettings.Load(); }
            catch (System.Exception) { }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("AdServer Settings", EditorStyles.boldLabel);

        settings.AdServerKey = EditorGUILayout.TextField("AdServer Key", settings.AdServerKey);
        settings.AdServerGoogleKey = EditorGUILayout.TextField("Google Key", settings.AdServerGoogleKey);
        settings.AdSdkVersion = EditorGUILayout.TextField("Ad SDK Version", settings.AdSdkVersion);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        GUILayout.Label("Android Auto Override Settings", EditorStyles.boldLabel);
        settings.AutoUpdateManifest = EditorGUILayout.Toggle("AndroidManifest.xml", settings.AutoUpdateManifest);
        settings.AutoUpdateBuildGradle = EditorGUILayout.Toggle("build.gradle", settings.AutoUpdateBuildGradle);
        settings.AutoUpdateSettingsGradle = EditorGUILayout.Toggle("settings.gradle", settings.AutoUpdateSettingsGradle);
        settings.AutoUpdateGradleProperties = EditorGUILayout.Toggle("gradle.properties", settings.AutoUpdateGradleProperties);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Save"))
        {
            settings.Save();
        }
    }

    public static AdServerSettings GetSettings()
    {
        if (settings == null)
        {
            settings = AdServerSettings.Load();
        }

        return settings;
    }
}
